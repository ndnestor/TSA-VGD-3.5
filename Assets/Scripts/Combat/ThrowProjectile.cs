using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowProjectile : MonoBehaviour {

    // Use this for initialization

    //Strength
    public int damage;
    //How long it lasts before getting destroyed (seconds)
    public float lifeSpan;
    //Speed
    public float velocity;
    //Maximum speed
    [HideInInspector] public float maxVelocity;
    //Whether or not it can do damage
    private bool canDamage = true;
    //Stores direction of project
    private Quaternion dir;
    //Keep track of when it was first spawned in
    private float saveTime;
    //If it was deflected
    private bool deflected = false;
    //The knockback force given to the player upon getting hit
    public int explosionForce;




    void Start()
    {
        //Initialize

        maxVelocity = velocity;
        //dir = gameObject.transform.parent.rotation;
        saveTime = Time.fixedTime;

    }

    void Update () {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        gameObject.transform.Translate( Vector3.forward * velocity * Time.deltaTime);
        //Safeguard against projectile sliding underneath the map.
        gameObject.transform.eulerAngles = new Vector3(0.0f, transform.eulerAngles.y, transform.eulerAngles.z);
        //gameObject.transform.Translate(0.0f, 0.1f - transform.position.y, 0.0f);
        //Delete the gameobject after some time
        if(Time.fixedTime-saveTime > lifeSpan){
            BreakProj();
        }
	}
    //Break the projectile
    void BreakProj () {
        //Maybe include some kind of animation depending on what the projectile is or a countdown.

        Destroy(gameObject);

    }

    void OnTriggerEnter(Collider other)
    {
        

        if (other.tag != "Enemy" && other.tag != "Weapon" && other.name != "Floor")
        {


            if(other.gameObject.name == "Dylan") {
                other.gameObject.GetComponent<CharacterInfo>().health -= damage;
                other.gameObject.GetComponent<CharacterMovement>().canMove = true;
                other.GetComponent<Rigidbody>().AddForce(transform.forward * explosionForce, ForceMode.VelocityChange);
                BreakProj();
            }
            BreakProj();

        }

        if(other.tag == "Enemy" && deflected){
            BreakProj();

            if (other.name != "Big Boss")
            {
                
                other.gameObject.GetComponent<EnemyInfo>().health = 0;
            }
            else {
                other.gameObject.GetComponent<EnemyInfo>().health -= 20;
            }

        }

        if (other.tag == "Weapon" && !deflected)
        {
            deflected = true;
            transform.Rotate(new Vector3(0.0f, 180.0f, 0.0f));
            //gameObject.transform.eulerAngles = new Vector3(0.0f, -transform.eulerAngles.y, transform.eulerAngles.z);
            //velocity *= -1;
        }

    }

}
