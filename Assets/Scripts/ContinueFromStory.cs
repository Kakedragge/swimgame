using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ContinueFromStory : MonoBehaviour {

	private float timeToShow = 2.0f;
	private Text t;
	private bool is_enabled = false;

	void Start(){
		t = GameObject.FindGameObjectWithTag("ExplainText"). GetComponent<Text>();	
	}

	// Update is called once per frame
	void Update () {

		if (timeToShow < 0) {

			if (!is_enabled) {
				ShowText ();
				is_enabled = true;
			}

			if (Input.anyKey) {
				SceneManager.LoadScene(1);
			}
		}
	}

	void ShowText(){
		t.enabled = true;
	}

	void FixedUpdate(){
		timeToShow -= Time.deltaTime;
	}
}
