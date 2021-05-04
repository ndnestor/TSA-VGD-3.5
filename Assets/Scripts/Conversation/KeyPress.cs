using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyPress : MonoBehaviour {

	[SerializeField] private KeyCode keyToPress;

	void Update() {
		if(Input.GetKeyDown(keyToPress)) {
			GetComponent<Image>().enabled = true;
		}
	}
}
