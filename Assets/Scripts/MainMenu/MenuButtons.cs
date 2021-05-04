using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour {

	//Menus to be activated or deactivated
	[SerializeField] private GameObject optionsMenu;
	[SerializeField] private GameObject creditsMenu;
	[SerializeField] private GameObject mainMenu;
	[SerializeField] private GameObject loadingScreen;

	//Load scene titled "Combat 1"
	public void StartGame() {
		loadingScreen.SetActive(true);
		mainMenu.SetActive(false);
		SceneManager.LoadScene("IntroCutscene", LoadSceneMode.Single);
	}

	//Show the options menu and hide the main menu
	public void Options() {
		optionsMenu.SetActive(true);
		mainMenu.SetActive(false);
	}

	//Show the credits menu and hide the main menu
	public void Credits() {
		creditsMenu.SetActive(true);
		mainMenu.SetActive(false);
	}

	//Show the main menu and hide the credits or options menu
	public void MainMenu() {
		mainMenu.SetActive(true);
		optionsMenu.SetActive(false);
		creditsMenu.SetActive(false);
	}

	//Quit to desktop (this does not work in the editor, only standalone)
	public void Exit() {
		Application.Quit();
	}
}
