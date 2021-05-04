using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour {

	[SerializeField] private int attackDistance;

    /*
     * 0 = Normal Behavior/Swarm player
     * 1 = Defensive - Stays a certain distance away from the player no matter what
     * 2 = Throws Projectiles - Also maintains a defensive behavior
     * 3 = Rushing - Charges when player is in line of sight, cools down, charges again
     * */
    [SerializeField] private int behavior;
    //Use for any enemies targeting the player with projectiles or maintaining a safe distance from player
    [SerializeField] private int avoidanceDist;
    //Use for enemies launching projectiles
    [SerializeField] private int launchFrequency;
    //Projectile launched at player
    [SerializeField] private GameObject projType;
    //Special Effects (explosions, etc...)
    [SerializeField] private GameObject sfx;
    //Drop Item
    [SerializeField] private GameObject drop;
    //The experience gained from killing this enemy
    [SerializeField] private int experienceGain;
    //The distance at which enemies will be activated at
    [SerializeField] private int engageDistance;

    //Has the player been seen
    private bool activate = false;

	private EnemyInfo enemyInfo;
	private NavMeshAgent agent;
	private GameObject[] enemies;
	private GameObject closest;

    //Directly associated with launchFrequency
    private float nextLaunch;

    //Variables for Rushing enemy
    private Ray sight;
    private RaycastHit hit;
    private bool detected = false;
    private bool crossedEnd = false;

    private RaycastHit hit2;


    //Only applies to swivel chair
    private float timeOfDeath;
    private bool dropped = false;
	void Start() {
		enemyInfo = GetComponent<EnemyInfo>();
		agent = GetComponent<NavMeshAgent>();

        nextLaunch = Time.fixedTime + launchFrequency;
        activate = true;
	}

	void FixedUpdate() {
        if (enemyInfo.health > 0)
        {
            timeOfDeath = Time.fixedTime;
        }
		GetClosestEnemy();
		agent.speed = enemyInfo.speed;
		agent.acceleration = enemyInfo.acceleration;
		agent.angularSpeed = enemyInfo.angularSpeed;

        /*
         * Detects if the player is in range of the robot. 
         * All AI uses this except for the rushing bot which has its
         * on detection
         * */
        if(behavior != 3 && closest != null && closest.GetComponent<CharacterInfo>() != null && !closest.GetComponent<CharacterInfo>().isDead){
            //Debug.DrawLine(transform.position, closest.transform.position);
            if(Physics.Linecast(transform.position, closest.transform.position, out hit2)){
                if (hit2.transform == closest.transform)
                {
                    
                    activate = true;
                }
            }

            if(Vector3.Distance(hit2.transform.position, closest.transform.position) < engageDistance) {
                activate = true;
            }

            NavMeshPath path = new NavMeshPath();
            if(agent.CalculatePath(closest.transform.position, path)) {
                activate = true;
            }


            if(behavior != 3 && closest != null && closest.GetComponent<CharacterInfo>() != null && !closest.GetComponent<CharacterInfo>().isDead){
                //Debug.DrawLine(transform.position, closest.transform.position);
                if(Physics.Linecast(transform.position, closest.transform.position, out hit2)){
                    if (hit2.transform == closest.transform)
                    {

                        activate = true;
                    }
                }
            }
        }

        if(behavior == 0 || behavior == 5 && activate){
            //Target the player
            transform.LookAt(closest.transform);
            agent.angularSpeed = 0;
            agent.destination = closest.transform.position;
        }
        if((behavior == 1 || behavior == 2 || behavior == 4) && activate){

            //Lock rotation
            agent.angularSpeed = 0;
            //Force the enemy to face the player always
            transform.LookAt(closest.transform);

            //Chase the player
            if (Vector3.Distance(closest.transform.position, transform.position) > avoidanceDist)
            {
                agent.destination = closest.transform.position;
            }
            //Once achieving a certain distance from the player stop running
            else if (Vector3.Distance(closest.transform.position, transform.position) <= avoidanceDist && Vector3.Distance(closest.transform.position, transform.position) > avoidanceDist-0.5){
                agent.destination = transform.position;
            }
            //If they player gets too close run away.
            else {
                //Uses a vector of the player to the enemy for running in the opposite direction.
                agent.destination += Vector3.Normalize(new Vector3(closest.transform.position.x - transform.position.x, 0.0f, closest.transform.position.z - transform.position.z)) * -0.5f;
            }

        }
        if(behavior == 2 && activate){
            
            if (Vector3.Distance(closest.transform.position, transform.position) < attackDistance && Time.fixedTime > nextLaunch){
                Instantiate(projType, new Vector3(transform.position.x, transform.position.y-0.2f, transform.position.z)+transform.forward*1.0f, Quaternion.Euler(90.0f, transform.eulerAngles.y, transform.eulerAngles.z));
                nextLaunch = Time.fixedTime + launchFrequency;
            }

        }
        if(behavior == 3){

            if(!detected){
                agent.destination = transform.position;
                transform.Rotate(new Vector3(0.0f, 2.0f, 0.0f));
            }

            Physics.Raycast(transform.position+transform.forward*2, transform.forward, out hit, attackDistance);

            //Debug.DrawRay(transform.position+transform.forward*2, transform.forward*10.0f);

            //Using LaunchFrequency to determine how often to charge. Uses less variables
            if(hit.collider != null && hit.collider.gameObject.tag == "Friendly"){
                if (!detected)
                {
                    agent.updateRotation = true;
                    agent.destination = closest.transform.position;
                    detected = true;
                }

            }
            if(Vector3.Distance(transform.position, agent.destination) < 2.0f){
                //The player has ran past their goal
                crossedEnd = true;
            }
            //If they get too far stop.

            /*
             * There are a few conditions that go into determining when the agent should stop.
             * 1. When they overrun their goal by a significant amount (2.0f)
             * 2. If the crossed over the end goal in the first place
             * Alternatively, the agent can cross over the end goal by only a slight amount if they have a very low
             * velocity. This prevents the agent from getting caught from constantly driving towards the same
             * goal position without ever resuming its spinning.
             * */
            if((Vector3.Distance(transform.position, agent.destination) > 2.0f || agent.velocity.x < 2) && crossedEnd){
                agent.destination = transform.position;
                detected = false;
                agent.updateRotation = false;
                crossedEnd = false;
            }
            //Debug.Log(Vector3.Distance(transform.position, agent.destination));
        }
        //Crowbar enemy
        if(behavior == 4 && activate){
            if (Vector3.Distance(closest.transform.position, transform.position) < attackDistance)
            {
                GetComponent<Weapon>().canStrikeAsEnemy = true;
            }
        }

		
        if(enemyInfo.health <= 0 && behavior != 5){
            if(drop != null){
                Instantiate(drop, transform.position, transform.Find("Model").rotation);
            }
            Instantiate(sfx, transform.position, Quaternion.Euler(new Vector3(90.0f, transform.eulerAngles.y, 0.0f)));
            if(closest != null && closest.GetComponent<CharacterInfo>() != null) {
                closest.GetComponent<CharacterInfo>().experience += experienceGain;
            }
            Destroy(gameObject);
        }
        else if(enemyInfo.health <= 0 && behavior == 5){
            if(!dropped){
                dropped = true;
                if(drop != null){
                    Instantiate(drop, transform.position, transform.Find("Model").rotation);
                }
            }
            if(Time.fixedTime-timeOfDeath > 3.1f){
                Destroy(gameObject);
            }
            else {
                GetComponent<Animator>().SetBool("isDead", true);
            }

           
        }

        if(closest != null && closest.GetComponent<CharacterInfo>() != null && closest.GetComponent<CharacterInfo>().isDead) {
            agent.destination = transform.position;
            activate = false;
        }
	}

	void GetClosestEnemy() {
		GetEnemies();
		closest = enemies[0];
		foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("Friendly")) {
			if(Vector3.Distance(enemy.transform.position, transform.position) < Vector3.Distance(closest.transform.position, transform.position)) {
				closest = enemy;
			}
		}
	}

	void GetEnemies() {
		enemies = GameObject.FindGameObjectsWithTag("Friendly");
	}

    void OnTriggerEnter(Collider other)
    {
        if(behavior == 3 && other.gameObject.tag == "Friendly"){
            other.GetComponent<CharacterInfo>().health -= 30;
            Instantiate(sfx, transform.position, Quaternion.Euler(new Vector3(90.0f, transform.eulerAngles.y, 0.0f)));
            Destroy(gameObject);

        }
        else if(behavior == 3 && other.gameObject.tag == "Enemy"){
            Instantiate(sfx, transform.position, Quaternion.Euler(new Vector3(90.0f, transform.eulerAngles.y, 0.0f)));
            Destroy(gameObject);
        }

    }
    private void OnTriggerStay(Collider other) {
        if (behavior == 0 && Time.fixedTime%1 <= 0.01f) {
            if(other.gameObject.name == "Dylan") {
                other.GetComponent<CharacterInfo>().health -= 10;
            }
        }
    }
}
