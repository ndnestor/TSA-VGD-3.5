using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverSystem : MonoBehaviour {

	[SerializeField] private GameObject CDOrigin;

	private RaycastHit rayHit;

	void Update() {

		//Create a ray from the Cover Detect Origin in the forwards direction
		Physics.Raycast(CDOrigin.transform.position, transform.forward, out rayHit);

		//If there is cover within 1 unit in front of character, use it
		if(rayHit.collider != null && rayHit.distance < 1 && rayHit.collider.gameObject.tag == "Standing Cover") {
			TakeStandingCover();
		} else if(rayHit.collider != null && rayHit.distance < 1 && rayHit.collider.gameObject.tag == "Crouching Cover") {
			TakeCrouchingCover();
		}
	}

	void TakeStandingCover() {
		//Animation and other stuff
	}

	void TakeCrouchingCover() {
		//Animation and other stuff
	}
}
