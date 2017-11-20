using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnTv : MonoBehaviour {

	private Component[] lights;
	// Use this for initialization
	void Start () {
		lights = GetComponentsInChildren<Light>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void TurnOn() {
		
		foreach (Light light in lights) 
		{
			light.enabled = true;
		}
	}

}
