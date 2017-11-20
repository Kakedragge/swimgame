using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visibility : MonoBehaviour {

	public Light lt;

	// Use this for initialization
	void Start () {
		lt = GetComponent<Light>();
	}

	// Update is called once per frame
	void Update () {
	}

	public void Darken() {
		lt.intensity -= lt.intensity / 1.1f * Time.deltaTime;
	}
}
