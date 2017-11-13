using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyFishMovement : MonoBehaviour {

	public Vector2 startPos;
	public Vector2 endPos;
	public float speed;

	private bool isMovingUP;

	// Use this for initialization
	void Start () {
		isMovingUP = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (isMovingUP) {
			transform.Translate (Vector2.up * speed * Time.deltaTime);
		} else {
			transform.Translate (-Vector2.up * speed * Time.deltaTime);
		}

		if (transform.position.y < startPos [1]) {
			isMovingUP = true;
		}

		if (transform.position.y > endPos [1]) {
			isMovingUP = false;
		}

	}
}
