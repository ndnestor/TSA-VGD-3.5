using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReportCollision : MonoBehaviour {

	public bool collided;

	void OnCollisionEnter(Collision collision) {
		if(collision.gameObject.name == "Door Cube") {
			collided = false;
		}
	}

	void OnCollisionExit() {
		collided = false;
	}
}
