using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Swiming : MonoBehaviour {

	public float speed = 2f;

	Rigidbody2D rb2D;

	// Use this for initialization
	void Start () {
		rb2D = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		rb2D.velocity = new Vector2 (moveHorizontal * speed, moveVertical * speed);
	}
}
