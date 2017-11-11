using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Swiming : MonoBehaviour {

	public float speed = 2f;

	Rigidbody2D rb2D;

	Animator anim;

	// Use this for initialization
	void Start () {
		rb2D = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
	}

	void FixedUpdate () {

		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		anim.SetFloat ("SwimX", moveHorizontal);
		anim.SetFloat ("SwimY", moveVertical);

		if (moveHorizontal != 0 || moveVertical != 0) {
			anim.SetBool ("Swimming", true);
		} else {
			anim.SetBool ("Swimming", false);
		}
			
		rb2D.velocity = new Vector2 (moveHorizontal * speed, moveVertical * speed);

	}
}
