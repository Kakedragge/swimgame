using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrigger : MonoBehaviour {


	private float wait;
	private bool endStarted = false;
	private float fadeTime = 5.0f;
	private bool hasEnded = false;


	// Use this for initialization
	void Start () {
		wait = 200 * Time.deltaTime;
	}
		
	// Update is called once per frame
	void Update () {
		if (endStarted) {
			wait -= 1 * Time.deltaTime;
			FindObjectOfType<Visibility> ().Darken ();
			if (wait < 0 && !hasEnded) {
				FindObjectOfType<Walking> ().DisableWalking ();
				FindObjectOfType<TurnOnTv> ().TurnOn ();
				FindObjectOfType<SoundManager> ().StopAllMusic ();
				FindObjectOfType<SoundManager> ().PlayTVSound ();
				hasEnded = true;
			}

		}
	}

	private float toPercent(float f){

		//set to fadetime

		float max = 10.0f;

		if (f / max < 0) {
			return 1;
		} else if (f / max > 1) {
			return 0;
		}

		return 1 - f / max;
	}

	void FixedUpdate(){
		if (hasEnded && fadeTime > 0) {
			fadeTime -= Time.deltaTime;
			FindObjectOfType<SoundManager> ().SetTVFade(toPercent(fadeTime));
		}
		else if(hasEnded && fadeTime < 0){
			FindObjectOfType<GameManager> ().Victory ();
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
