﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstCutscene : MonoBehaviour {

	public void ToGameScene() {
		SceneManager.LoadScene("ActualMap", LoadSceneMode.Single);
	}

}
