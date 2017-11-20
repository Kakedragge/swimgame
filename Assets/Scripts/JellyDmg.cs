using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyDmg : MonoBehaviour {

	public int damage;
	private Animator anim;
	private bool stunned;
	private float timeleft;
	private GameObject jellyFish;
	private Vector3 hey;


	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		stunned = false;
		timeleft = 1.0f;
	}

	// Update is called once per frame
	void Update () {
		SendMessage("setStunned", stunned);
		if (stunned) {
			timeleft -= Time.deltaTime;

			hey = Vector2.MoveTowards (new Vector2 (transform.position.x, transform.position.y), new Vector2 (jellyFish.transform.position.x, jellyFish.transform.position.y), -0.3f * Time.deltaTime);
			transform.position = new Vector3 (hey.x, hey.y, transform.position.z);

			if (timeleft < 0) {
				anim.SetBool ("Stunned", false);
				SendMessage ("setStunned", true);
				stunned = false;
				timeleft = 0.7f;
			}
		}
	}

	private void Stunn () {
		if (!stunned) {
			anim.SetBool ("Stunned", true);
			stunned = true;

			GameObject SwimMan = GameObject.Find("SwimMan");
			FindObjectOfType<SoundManager> ().PlaySpark();
			SendMessage("setStamina", SwimMan.GetComponent<AirManagement>().getStamina() - damage);
		}
	}

	void OnTriggerEnter2D(Collider2D other) 
	{
		if (other.gameObject.CompareTag ("Jelly Fish")) 
		{
			jellyFish = other.gameObject;
			Stunn ();
		}
	}

}
