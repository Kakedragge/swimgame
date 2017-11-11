using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hiding : MonoBehaviour {

	private bool isHiding = false;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

	}

	void OnTriggerEnter2D(Collider2D other) 
	{
		if (other.gameObject.CompareTag ("Hide Element")) 
		{
			isHiding = true;
			Debug.Log (isHiding.ToString());
		}
	}

	void OnTriggerExit2D(Collider2D other) 
	{
		if (other.gameObject.CompareTag ("Hide Element")) 
		{
			isHiding = false;
			Debug.Log (isHiding.ToString());
		}
	}


}
