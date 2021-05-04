using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour {

	[SerializeField] private SpriteRenderer grassRenderer;
	[SerializeField] private float animationSpeed;
	[SerializeField] private Sprite[] grassSprites;

	private float animationTime = 0;
	private int spriteIndex;

	void FixedUpdate() {
		animationTime += Time.fixedDeltaTime;

		if(animationTime > animationSpeed) {
			NextSprite();
			animationTime = 0;
		}
	}

	void NextSprite() {
		spriteIndex++;
		if(spriteIndex > grassSprites.Length - 1) {
			spriteIndex = 0;
		}
		grassRenderer.sprite = grassSprites[spriteIndex];
	}
}
