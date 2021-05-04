using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextPanel : MonoBehaviour {

	[SerializeField] private string[] messages;
	[SerializeField] private Sprite[] messengers;
	[SerializeField] private string[] actions;
	[SerializeField] private Text messageText;
	[SerializeField] private Image currMessengerImage;
	[SerializeField] private GameObject keyTutorial;
	[SerializeField] private GameObject attackTutorial;
	[SerializeField] private GameObject reflectTutorial;
	[SerializeField] private GameObject player;

	private Manager manager;
	private int index = 0;
	private bool waitingForAction;
	private string lastAction;

	void Start() {
		manager = GameObject.Find("__Manager__").GetComponent<Manager>();
		NextMessage();
	}

	void Update() {
		if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)) {
			if(index > messages.Length - 1) {
                Destroy(gameObject);
				gameObject.SetActive(false);
			} else if(!waitingForAction) {
				NextMessage();
			}
		}

		if(lastAction == "Move Prompt") {
			MovePrompt();
		} else if(lastAction == "Attack Prompt") {
			AttackPrompt();
		} else if(lastAction == "Reflect Prompt") {
			ReflectPrompt();
		}
	}

	void UpdateMessage(string message, Sprite messenger, string action) {
		messageText.text = message;
		currMessengerImage.sprite = messenger;

		lastAction = action;
	}

	void NextMessage() {
		UpdateMessage(messages[index], messengers[index], actions[index]);
		index++;
        if(index > messages.Length){
            Destroy(gameObject);
        }
	}

	//For use with MovePrompt()
	bool up = false;
	bool left = false;
	bool down = false;
	bool right = false;

	void MovePrompt() {

		if(!waitingForAction && keyTutorial != null) {
			keyTutorial.SetActive(true);			
		}

		waitingForAction = true;


		if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown("w")) {
			up = true;
		}
		if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown("a")) {
			left = true;
		}
		if(Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown("s")) {
			down = true;
		}
		if(Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown("d")) {
			right = true;
		}

		if(up && left && down && right) {
			lastAction = "";
			keyTutorial.SetActive(false);
			waitingForAction = false;
			NextMessage();
		}
	}

	void AttackPrompt() {
		if(!waitingForAction) {
			attackTutorial.SetActive(true);
		}

		if(Input.GetMouseButtonDown(0) && waitingForAction) {
			lastAction = "";
			attackTutorial.SetActive(false);
			waitingForAction = false;
			NextMessage();
		} else {
			waitingForAction = true;
		}
	}

	void ReflectPrompt() {
		GameObject[] projectiles = GameObject.FindGameObjectsWithTag("Projectile");

		waitingForAction = true;

		foreach(GameObject projectile in projectiles) {
			if(Vector3.Distance(projectile.transform.position, player.transform.position) < 4) {

				projectile.GetComponent<ThrowProjectile>().velocity = 0;
				Time.timeScale = 0;
				reflectTutorial.SetActive(true);

				/*if(Input.GetMouseButtonDown(0)) {
					Debug.Log(Vector2.Distance(new Vector2(projectile.transform.position.x, projectile.transform.position.y),
											   Camera.main.ScreenToWorldPoint(Input.mousePosition)));
				}*/

				if(Input.GetMouseButtonDown(0) &&
					Vector2.Distance(new Vector2(projectile.transform.position.x, projectile.transform.position.y),
												 Camera.main.ScreenToWorldPoint(Input.mousePosition)) < 32) {
					Debug.Log("It should work");
					projectile.GetComponent<ThrowProjectile>().velocity = projectile.GetComponent<ThrowProjectile>().maxVelocity;
					Time.timeScale = 1;
					lastAction = "";
					reflectTutorial.SetActive(false);
					waitingForAction = false;
					NextMessage();
				}
			}
		}
	}

	void Win() {
		manager.NextScene();
	}
}
