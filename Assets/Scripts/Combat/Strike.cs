using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strike : MonoBehaviour
{

    public int damage;

    //Used to limit any weapon to damaging an enemy once
    private bool collided;
    private Transform parentModel;
    //NOTE: All models that are to be held by the player must be 0 degrees on x-axis as it will be rotated as a child of Model (Dylan)
    void Start()
    {
        parentModel = transform.parent;
        collided = false;
    }


    void Update()
    {

        //Debug.Log(damage);
        if(parentModel.parent.GetComponent<Weapon>().timeSinceLastShot > 0.5f){
            
            Destroy(gameObject);

        }

    }
    void OnTriggerEnter(Collider other)
    {
        
        if (!collided)
        {
            if (parentModel.tag == "Friendly" && other.gameObject.tag == "Enemy") {
                if (other.gameObject.name != "Big Boss")
                {
                    other.GetComponent<EnemyInfo>().health -= damage;

                }
                else {
                    other.GetComponent<EnemyInfo>().health -= damage / 3;
                }
                collided = true;
            }
            else if(parentModel.tag == "Enemy" && other.gameObject.tag == "Friendly") {
                
                other.GetComponent<CharacterInfo>().health -= damage / 3;
                collided = true;
            }


        }

    }
}
