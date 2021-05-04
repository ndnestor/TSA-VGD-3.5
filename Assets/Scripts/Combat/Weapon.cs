using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

	[SerializeField] private float cooldownTime;
    [SerializeField] private float strength;

	public float timeSinceLastShot;
	public bool coolingDown;
    //Only for enemies using weapons
    public bool canStrikeAsEnemy;


    private GameObject weapon;



    void Update() {
        //This is in update in case the player's weapon changes during gameplay.
        if (tag == "Friendly")
        {
            weapon = GetComponent<CharacterInfo>().weapon;
        }
        else if (tag == "Enemy")
        {
            weapon = GetComponent<EnemyInfo>().weapon;
        }
		if(coolingDown) {
            
			timeSinceLastShot += Time.fixedDeltaTime;
			if(timeSinceLastShot > cooldownTime) {
                

				coolingDown = false;

			}
        }
        if (Input.GetMouseButtonDown(0) && !coolingDown && tag == "Friendly" && !GetComponent<CharacterInfo>().isDead && weapon != null)
        {
            
            Instantiate(weapon, new Vector3(transform.position.x, transform.position.y - 0.3f, transform.position.z+0.1f) + transform.Find("Model").right + transform.Find("Model").up*0.6f, transform.rotation, transform.Find("Model"));
            timeSinceLastShot = 0.0f;
            coolingDown = true;

        }
        else if(canStrikeAsEnemy && !coolingDown){
            Instantiate(weapon, new Vector3(transform.position.x, transform.position.y + 2.0f, transform.position.z) + transform.Find("Model").right + transform.Find("Model").up, transform.rotation, transform.Find("Model"));
            timeSinceLastShot = 0.0f;
            coolingDown = true;
        }
	}
}
