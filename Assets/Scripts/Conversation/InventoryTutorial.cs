using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryTutorial : MonoBehaviour {

	[SerializeField] private GameObject inventoryTutorial;
	[SerializeField] private GameObject key;
	[SerializeField] private GameObject keySpawn;
	private bool isActive;

	void FixedUpdate() {

		if(GetComponent<CharacterInfo>().inventory[0] != null) {
			inventoryTutorial.SetActive(true);
			isActive = true;
		}
	}

	void Update() {
		if(isActive && Input.GetKeyDown(KeyCode.Tab)) {
			inventoryTutorial.SetActive(false);
			Instantiate(key, keySpawn.transform.position, keySpawn.transform.rotation);
			isActive = false;
			this.enabled = false;
		}
	}
}
