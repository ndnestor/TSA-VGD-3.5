using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableOnCollision : MonoBehaviour {

	[SerializeField] GameObject GOToEnable; //The game object to enable
	[SerializeField] GameObject GOToCollide; //Leave empty to allow any Game Object to work

	
    private void OnTriggerEnter(Collider other)
    {
        if (GOToCollide == null)
        {
            GOToEnable.SetActive(true);
        }
        else if (other.gameObject == GOToCollide)
        {
            GOToEnable.SetActive(true);
        }
    }

}
