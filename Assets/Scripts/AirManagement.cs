using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AirManagement : MonoBehaviour {

	public float stamina;
	public float stm;

	private SpriteRenderer spriteRenderer;
	private Color oldColor;

	// Use this for initialization
	void Start () {
        stamina = stm * Time.deltaTime;
		spriteRenderer = GetComponent<SpriteRenderer> ();
		oldColor = spriteRenderer.color;
		FindObjectOfType<HealthBar>().StartUp(stamina);
	//	SetStaminaText ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if (stamina <= 0)
        {
            FindObjectOfType<GameManager>().RestartGame();
        }
        else if (FindObjectOfType<Swiming>().IsUnderWater())
        {
			stamina -= 1 * Time.deltaTime;

			spriteRenderer.color = new Color (spriteRenderer.color [0], spriteRenderer.color [1], 0.1f + ((Time.deltaTime*stm - stamina)/(Time.deltaTime*stm))/5.0f);
			FindObjectOfType<HealthBar>().UpdateHealthBar(stamina);

        }
        else
        {
			spriteRenderer.color = oldColor;
			FindObjectOfType<HealthBar>().UpdateHealthBar(stamina);
        }

    }

    public float getStamina()
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
			stamina = stm * Time.deltaTime;
		}
	}
		

}
