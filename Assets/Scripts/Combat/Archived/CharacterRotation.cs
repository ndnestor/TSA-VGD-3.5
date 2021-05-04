using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRotation : MonoBehaviour {

	[SerializeField] private GameObject LOSOrigin;

	private GameObject[] enemies;
	private GameObject[] enemiesInLineOfSight; //Enemies that this character can see
	private GameObject preferredTarget; //The enemy that is closest to the character
	private string enemyTag;
	private RaycastHit hit;

	void FixedUpdate() {

		//Define which characters are enemies
		if(gameObject.tag == "Friendly") {							//TODO: Clean this up (18-24)
			enemies = GameObject.FindGameObjectsWithTag("Enemy");
			enemyTag = "Enemy";
		} else if(gameObject.tag == "Enemy") {
			enemies = GameObject.FindGameObjectsWithTag("Friendly");
			enemyTag = "Friendly";
		}

		//Update some info
		UpdateEnemiesInLineOfSight();
		UpdatePreferredTarget();

		//Look at the preferred target if there is one
		if(preferredTarget != null) {
			transform.LookAt(preferredTarget.transform, Vector3.up);
			transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, transform.eulerAngles.z);
		} else {
			transform.localEulerAngles = new Vector3(0, 0, 0);
		}
	}

	//Check to see which enemies are within the character's line of sight
	void UpdateEnemiesInLineOfSight() {
		enemiesInLineOfSight = new GameObject[enemies.Length];
		for(int i = 0; i < enemies.Length; i++) {
			int n = 0;

			Physics.Linecast(LOSOrigin.transform.position, enemies[i].transform.position, out hit);
			if(hit.collider.gameObject.tag == enemyTag) {
				enemiesInLineOfSight[n] = enemies[i];
				n++;
			}
		}
	}

	//Check to see which enemy within the character's line of sight is the closest
	void UpdatePreferredTarget() {
		foreach(GameObject h in enemiesInLineOfSight) {
			if(enemiesInLineOfSight.Length == 0) {
				preferredTarget = null;
			} else if(enemiesInLineOfSight[0] == null) {
				preferredTarget = null;
			} else if(preferredTarget == null) {
				preferredTarget = h;
			} else if(Vector3.Distance(h.transform.position, transform.position) <
					  Vector3.Distance(preferredTarget.transform.position, transform.position)) {

				preferredTarget = h;
			}
		}
	}
}
