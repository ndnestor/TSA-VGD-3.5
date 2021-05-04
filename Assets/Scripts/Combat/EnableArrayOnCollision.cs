using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableArrayOnCollision : MonoBehaviour {

	[SerializeField] GameObject[] GOToEnable; //The game object to enable
	[SerializeField] GameObject GOToCollide; //Leave empty to allow any Game Object to work

	
    private void OnTriggerEnter(Collider other)
    {
        if (GOToCollide == null)
        {
        	foreach(GameObject go in GOToEnable) {
                if(go != null) {
            	   go.SetActive(true);
                }
        	}
        }
        else if (other.gameObject == GOToCollide)
        {
        	foreach(GameObject go in GOToEnable) {
                if(go != null) {
            	   go.SetActive(true);
                }
        	}
        }
    }

}
