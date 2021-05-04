using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasResizer : MonoBehaviour {

	private float targetSize;

	void Update() {
		targetSize = Camera.main.orthographicSize * 2.0f * Screen.width / Screen.height;
		transform.localScale = new Vector3(targetSize / 800, targetSize / 800, targetSize / 800);
	}
    
}
