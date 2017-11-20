using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyDmg : MonoBehaviour {

	public int damage;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other) 
	{
		if (other.gameObject.CompareTag ("Jelly Fish")) 
		{
			GameObject SwimMan = GameObject.Find("SwimMan");
			FindObjectOfType<SoundManager> ().PlaySpark();
			SendMessage("setStamina", SwimMan.GetComponent<AirManagement>().getStamina() - damage);
		}
	}

}
