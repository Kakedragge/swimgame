using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

	private Image healthBar;
	private float maxHp;
	private float currentHp;
	private float ratio;
	// Use this for initialization
	void Start () {
		healthBar = GameObject.FindGameObjectWithTag("hptag").GetComponent<Image> ();
		maxHp = 1000;
		currentHp = 1000;
		ratio = currentHp / maxHp;
		healthBar.rectTransform.localScale = new Vector3 (ratio, 1.0f, 1.0f);
		healthBar.color = new Color32 (0, 0, 255, 150);
	}

	public float findLowHealthPercent(float value){

		float retVal = 1 - value / 0.25f;

		if (retVal <= 0) {
			retVal = 0;
		} else if (retVal > 1) {
			retVal = 1;
		}

		print ("Retval " + retVal); 

		return retVal;
	}
	
	// Update is called once per frame
	public void UpdateHealthBar (float hp) {
		
		currentHp = hp;
		ratio = currentHp / maxHp;
		healthBar.rectTransform.localScale = new Vector3 (ratio, 1.0f, 1.0f);
		FindObjectOfType<SoundManager> ().UpdateHeartbeat (findLowHealthPercent (ratio));

		if (ratio <= 0.25) {
			healthBar.color = new Color32 (255, 0, 0, 255);
		} else if (ratio > 0.25) {
			healthBar.color = new Color32 (0, 0, 255, 150);
		}
	}

	public void FullHealthBar(){
		currentHp = 1000;
		ratio = currentHp / maxHp;
		healthBar.color = new Color32 (0, 0, 255, 150);
		FindObjectOfType<SoundManager> ().UpdateHeartbeat (0.0f);
		healthBar.rectTransform.localScale = new Vector3 (ratio, 1.0f, 1.0f);
	}
}
