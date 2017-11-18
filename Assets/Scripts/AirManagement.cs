using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AirManagement : MonoBehaviour {

	public float stamina;
	public Text staminaText;

	private bool isDead = false;
	private bool isWater = true;



	// Use this for initialization
	void Start () {
		FindObjectOfType<HealthBar> ().FullHealthBar();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (stamina <= 0) {
			FindObjectOfType<GameManager> ().RestartGame ();
		} else if (FindObjectOfType<Swiming> ().IsUnderWater ()) {
			stamina -= 1;
			FindObjectOfType<HealthBar> ().UpdateHealthBar (stamina);
		} else {
			FindObjectOfType<HealthBar> ().FullHealthBar();
		}
	}
		
	public float getStamina(){
		return stamina;
	}

    public void setStamina(float newStamina)
    {
        stamina = newStamina;
    }

    void OnTriggerEnter2D(Collider2D other) 
	{
		if (other.gameObject.CompareTag ("Air Pocket")) 
		{
			stamina = 1000;
			isWater = false;
		}
	}
}
