using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour {

	//A list of scene names in the order that they will be shown to the player
	[SerializeField] private string[] sceneNames;
	//The index of the current scene
	private int sceneIndex;

	void Start() {
		//Keeps this game object in the game even through scene chagnes
		DontDestroyOnLoad(this.gameObject);
	}

	public void NextScene() {
		SceneManager.LoadScene(sceneNames[sceneIndex + 1], LoadSceneMode.Single);
		sceneIndex++;
	}
}
