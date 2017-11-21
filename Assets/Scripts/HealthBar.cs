using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

	private Image healthBar;
	private float maxHp;
	private float currentHp;
	private float ratio;
	private float dangerLow;
	// Use this for initialization

	void Start() {
		healthBar = GameObject.FindGameObjectWithTag("HP").GetComponent<Image> ();
		dangerLow = 0.25f;
		maxHp = 40;
		FullHealthBar ();
	}

	public void StartUp (float hp) {
		maxHp = hp;
		FullHealthBar ();
	}
	
	// Update is called once per frame
	public void UpdateHealthBar (float hp) {
		currentHp = hp;
		ratio = currentHp / maxHp;
		healthBar.rectTransform.localScale = new Vector3 (ratio, 1.0f, 1.0f);
		FindObjectOfType<SoundManager> ().UpdateHeartBeat (FindHearthBeatPercent(ratio));
		if (ratio <= dangerLow) {
			healthBar.color = new Color32 (255, 0, 0, 255);
		} else if (ratio > dangerLow) {
			healthBar.color = new Color32 (0, 0, 255, 150);
		}
	}

	public float FindHearthBeatPercent(float value) {
		float retVal = 1 - value / dangerLow;

		if (retVal <= 0) {
			retVal = 0;
		} else if (retVal > 1) {
			retVal = 1;
		} 
		return retVal;
	}

	public void FullHealthBar () {
		currentHp = maxHp;
		ratio = currentHp / maxHp;
		healthBar.color = new Color32 (0, 0, 255, 150);
		healthBar.rectTransform.localScale = new Vector3 (ratio, 1.0f, 1.0f);
		FindObjectOfType<SoundManager> ().UpdateHeartBeat (0.0f);
	}
		
}
