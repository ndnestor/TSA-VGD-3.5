using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoint : MonoBehaviour {
	//NOTE: THE GAMEOBJECT WITH THIS SCRIPT ATTACHED MUST HAVE THE "CHECKPOINT" LAYER AND A COLLIDER

	
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Friendly"){

            SceneManager.LoadScene("MainMenu(Revised)", LoadSceneMode.Single);
        }
    }
}
