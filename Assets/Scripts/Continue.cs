using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Continue : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		if (Input.anyKey) {
			FindObjectOfType<VictoryScreenMusic> ().StopVictorySound ();
			SceneManager.LoadScene(0);
		}
	}
}
