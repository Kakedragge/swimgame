using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrigger : MonoBehaviour {


	private float wait;
	private bool endStarted = false;

	// Use this for initialization
	void Start () {
		wait = 200 * Time.deltaTime;
	}
		
	// Update is called once per frame
	void Update () {
		if (endStarted) {
			wait -= 1 * Time.deltaTime;
			FindObjectOfType<Visibility> ().Darken ();
			if (wait < 0) {
				FindObjectOfType<TurnOnTv> ().TurnOn ();
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other) 
	{
		if (other.gameObject.CompareTag ("player")) 
		{
			FindObjectOfType<SteinFaller> ().FallDown(true);
			endStarted = true;

		}
	}
}
