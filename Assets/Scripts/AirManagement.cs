using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AirManagement : MonoBehaviour {

	public int stamina;
	public Text staminaText;

	private bool isDead = false;
	private bool isWater = true;

	// Use this for initialization
	//void Start () {
	//	SetStaminaText ();
	//}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (stamina >= 0) {
			isDead = true;
		}
		if (isWater == true) {
			stamina -= 1;
		}
		//SetStaminaText ();
	}

    public int getStamina()
    {
        return stamina;
    }


    public void setStamina(int newStamina)
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

	void OnTriggerExit2D(Collider2D other) 
	{
		if (other.gameObject.CompareTag ("Air Pocket")) 
		{
			isWater = true;
		}
	}

	//void SetStaminaText() {
	//	staminaText.text = "Air: " + stamina.ToString();
	//}
}
