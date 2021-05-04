using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToDir : MonoBehaviour {

	[SerializeField] private CharacterMovement characterMovement;
	[SerializeField] private ReportCollision collisionReport;

	void FixedUpdate() {
		transform.LookAt(transform.position + characterMovement.dir);

		if(collisionReport.collided == true) {
			characterMovement.canMove = false;
		} else {
			characterMovement.canMove = true;
		}
	}
}
