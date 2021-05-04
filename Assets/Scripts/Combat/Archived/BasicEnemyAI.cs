using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyAI : MonoBehaviour {

	/*
	 * 0 - 
	 * */
    [SerializeField] private int behavior;
    //Movement speed
    [SerializeField] private int speed;
    //Maximum range an enemy can wander from its starting position
    [SerializeField] private int walkRange;
    //How close the enemy must be to attack the player. (-1 = attack always)
    [SerializeField] private int targetRange;
    //Frequency of attacks (seconds)
    [SerializeField] private int cooldown;
    [SerializeField] private int chargeDuration;

    private GameObject[] enemies;
    private GameObject closest;
    private Vector3 movement;
    private float inital_y;
    private bool charging = false;
    private float launchTime;


	void Start () {
        launchTime = Time.fixedTime + cooldown;
        //Code from EnemyAI class
        enemies = GameObject.FindGameObjectsWithTag("Friendly");
        closest = enemies[0];
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Friendly"))
        {
            if (Vector3.Distance(enemy.transform.position, transform.position) < Vector3.Distance(closest.transform.position, transform.position))
            {
                closest = enemy;
            }
        }
        inital_y = transform.position.y;


	}
	
	// Update is called once per frame
	void Update () {
        if(Vector3.Distance(transform.position, closest.transform.position) < targetRange){
            if(behavior == 0){
                calcPath();
                transform.Translate(movement * speed * 0.01f);
            }
            if(behavior == 1){
                //transform.LookAt(closest.transform);
                if (!charging && Time.fixedTime > launchTime)
                {
                    
                    calcPath();
                    charging = true;
                    launchTime = Time.fixedTime + chargeDuration;

                }
                if (charging)
                {
                    
                    charge();
                }
            }
        }
	}

    void calcPath(){
        movement = Vector3.Normalize(new Vector3(closest.transform.position.x - transform.position.x, transform.position.y-inital_y, closest.transform.position.z - transform.position.z));
    }
    void charge(){
        
        if(Time.fixedTime < launchTime){
            transform.Translate(movement * speed * 0.01f);
        }
        else {
            launchTime = Time.fixedTime + cooldown;
            charging = false;

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        
        Debug.Log(other);
        if (other.gameObject.tag != "Enemy" && other.gameObject.tag != "Friendly" && other.gameObject.name != "Terrain")
        {
            charging = false;
            launchTime = Time.fixedTime + cooldown;


        }

    }

}
