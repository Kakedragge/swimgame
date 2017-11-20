using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteinFaller : MonoBehaviour {

	private Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		anim.enabled = false;
	}

	public void FallDown (bool fall) 
	{
		anim.enabled = true;

	}

	// Update is called once per frame
	void Update () {
		
	}
}
