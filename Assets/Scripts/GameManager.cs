using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	bool hasEnded = false;
	public float timeOut = 2f;
	public Animator anim;
	public Image black;

	public void EndGame(){
		if (hasEnded == false) {
			hasEnded = true;
			print ("Game over");
			StartCoroutine (Fading ());
		}

	}

    void Restart()
    {
		StopMusic ();
        SceneManager.LoadScene(0);

    }

    void StopMusic()
    {
		FindObjectOfType<SoundManager> ().StopAllMusic ();
    }

	IEnumerator Fading(){
		anim.SetBool ("Fade", true);
		yield return new WaitUntil(()=>black.color.a==1);
		Restart();
	}
}
