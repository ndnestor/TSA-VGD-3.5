using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : MonoBehaviour {

	[SerializeField] private int damageModifier;
	[SerializeField] private int healthModifier;
	[SerializeField] private int speedModifier;
	[SerializeField] private GameObject levelUpPrompt;

	private CharacterInfo characterInfo;

	void Start() {
		characterInfo = GetComponent<CharacterInfo>();
	}

	void FixedUpdate() {
		if(characterInfo.experience > 300 * characterInfo.level) {
			IncreaseLevel();
		}

	}

	void IncreaseLevel() {
		Debug.LogError("Increase level is called");
		levelUpPrompt.SetActive(true);
		Debug.LogError("State: " + levelUpPrompt.activeSelf);
		characterInfo.level++;
		Time.timeScale = 0;
	}

	public void IncreaseAttack() {
		characterInfo.weapon.GetComponent<Strike>().damage += damageModifier;
		Time.timeScale = 1;
		levelUpPrompt.SetActive(false);
	}

	public void IncreaseHealth() {
		characterInfo.maxHealth += healthModifier;
		characterInfo.health += healthModifier;
		Time.timeScale = 1;
		levelUpPrompt.SetActive(false);
	}

	public void IncreaseSpeed() {
		characterInfo.force += speedModifier;
		Time.timeScale = 1;
		levelUpPrompt.SetActive(false);
	}
}
