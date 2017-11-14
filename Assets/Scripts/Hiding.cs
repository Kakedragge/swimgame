using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hiding : MonoBehaviour {

	GameObject Shark;

	// Use this for initialization
	void Start () {
		Shark = GameObject.Find("Shark");
	}
	
	// Update is called once per frame
	void FixedUpdate () {

	}

	void OnTriggerEnter2D(Collider2D other) 
	{
		if (other.gameObject.CompareTag ("Hide Element")) 
		{
			Shark.SendMessage ("setIsHiding", true);
		}
	}

	void OnTriggerExit2D(Collider2D other) 
	{
		if (other.gameObject.CompareTag ("Hide Element")) 
		{
			Shark.SendMessage ("setIsHiding", false);
		}
	}


}
