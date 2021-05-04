using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	[SerializeField] private int moveSpeed;
	[SerializeField] private int rotateSpeed;
	[SerializeField] private int scrollSpeed;

	[SerializeField] private int FOVMin;
	[SerializeField] private int FOVMax;

	private Camera mainCamera;

	void Awake() {
		mainCamera = Camera.main;
	}

	void Update() {
		//Move mainCamera with WASD or arrow keys
		if(Input.GetAxis("Vertical") != 0) {
			transform.Translate(new Vector3(0, 0, moveSpeed * Input.GetAxis("Vertical") * Time.deltaTime));
		}
		if(Input.GetAxis("Horizontal") != 0) {
			transform.Translate(new Vector3(moveSpeed * Input.GetAxis("Horizontal") * Time.deltaTime, 0, 0));
		}

		//Rotate mainCamera with Q and E
		if(Input.GetKey("q")) {
			transform.RotateAround(transform.position, Vector3.up, -rotateSpeed * Time.deltaTime);
		}
		if(Input.GetKey("e")) {
			transform.RotateAround(transform.position, Vector3.up, rotateSpeed * Time.deltaTime);
		}

		//Zoom in and out with scroll wheel
		if(Input.GetAxis("Mouse ScrollWheel") > 0 && mainCamera.fieldOfView >= FOVMin) {
			mainCamera.fieldOfView += scrollSpeed * -Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime;
		} else if(Input.GetAxis("Mouse ScrollWheel") < 0 && mainCamera.fieldOfView <= FOVMax) {
			mainCamera.fieldOfView += scrollSpeed * -Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime;
		}
	}
}
