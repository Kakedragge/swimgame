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
		healthBar = GetComponent<Image> ();
		maxHp = 1000;
		currentHp = 1000;
		ratio = currentHp / maxHp;
		healthBar.rectTransform.localScale = new Vector3 (ratio, 1.0f, 1.0f);
		healthBar.color = new Color32 (0, 0, 255, 150);
	}
	
	// Update is called once per frame
	public void UpdateHealthBar (float hp) {
		currentHp = hp;
		ratio = currentHp / maxHp;
		healthBar.rectTransform.localScale = new Vector3 (ratio, 1.0f, 1.0f);
		if (ratio <= 0.25) {
			healthBar.color = new Color32 (255, 0, 0, 255);
		} else if (ratio > 0.25) {
			healthBar.color = new Color32 (0, 0, 255, 150);
		}
	}

	public void FullHealthBar(){
		currentHp = 1000;
		ratio = currentHp / maxHp;
		healthBar.rectTransform.localScale = new Vector3 (ratio, 1.0f, 1.0f);
	}
}
