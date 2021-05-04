using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {

	[SerializeField] private int force;
	[SerializeField] private int maxVelocity;
	[SerializeField] private Rigidbody rb;
	[SerializeField] private float raycastRadius;
	[SerializeField] private Animator animator;

	private float horizontalVector;
	private float verticalVector;
	private RaycastHit rayHit;

	public bool canMove;
	public Vector3 dir;


	void Update() {
        
		//WASD and arrow key input
		if(Input.GetAxis("Horizontal") != 0) {
			horizontalVector = force * Input.GetAxis("Horizontal");
		} else {
			horizontalVector = -rb.velocity.x;
		}

		if(Input.GetAxis("Vertical") != 0) {
			verticalVector = force * Input.GetAxis("Vertical");
		} else {
			verticalVector = -rb.velocity.y;
		}



		//Prevent translating into objects
		dir = new Vector3(horizontalVector, 0, verticalVector);


		//Translate charater based on keyboard input
        if(canMove) {
            rb.AddForce(dir * Time.deltaTime, ForceMode.Impulse);

            if(dir.magnitude > 0.1) {
				animator.SetBool("isWalking", true);
			} else {
				animator.SetBool("isWalking", false);
			}
		}

	}
}
