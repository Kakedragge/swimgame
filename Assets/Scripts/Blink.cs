using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blink : MonoBehaviour {

	Text t;
	float value;

	// Use this for initialization
	void Start () {
		t = GameObject.FindGameObjectWithTag("ExplainText"). GetComponent<Text>();
	}

	void FixedUpdate(){
		t.color = Color.Lerp (Color.white, Color.black, Mathf.PingPong (Time.time, 1));
	}
	

}
