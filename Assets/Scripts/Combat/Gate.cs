using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour {

	private Inventory inventory;

	void Start() {
		inventory = GameObject.Find("Full Inventory").GetComponent<Inventory>();
	}

    void OnCollisionEnter(Collision collision) {

    	if(collision.collider.gameObject.name == "Dylan") {
    		foreach(Sprite sprite in inventory.data) {
                if(sprite != null && (sprite.name == "Key" || sprite.name == "Gold Key")) {
    				inventory.RemoveKey();
    				Destroy(transform.parent.gameObject); //Alternatively we could play an animation of the gate opening
    			}										 //but that is ONLY if we have enough time
    		}
    	}
    }
}
