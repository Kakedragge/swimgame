using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	bool hasEnded = false;
	public float timeOut = 2f;
	private Animator anim;
	public Image black;
	public Transform canvas;


	void Start(){
		anim = GameObject.FindGameObjectWithTag("FadeImage"). GetComponent<Animator>();
		black = GameObject.FindGameObjectWithTag ("FadeImage").GetComponent<Image> ();
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (canvas.gameObject.activeInHierarchy == false) {
				canvas.gameObject.SetActive (true);
				PauseMusic ();
				Time.timeScale = 0;
			} 
			else {
				canvas.gameObject.SetActive (false);
				ResumeMusic ();
				Time.timeScale = 1;
			}
		}
	}

	public void Continue(){
		canvas.gameObject.SetActive (false);
		Time.timeScale = 1;
	}

	public void RestartGame(){
		if (hasEnded == false) {
			hasEnded = true;
			print ("Game over");
			StopMusic ();
			FindObjectOfType<SoundManager> ().PlayDeath ();
			StartCoroutine (Fading (false, 1));
		}

	}

	public void MainMenu(){
		if (hasEnded == false) {
			hasEnded = true;
			Time.timeScale = 1;
			StopMusic ();
			StartCoroutine (Fading (false, 0));
		}
	}

	public void Quit(){
		if (hasEnded == false) {
			hasEnded = true;
			Time.timeScale = 1;
			StartCoroutine (Fading (true, -1));
		}
	}

	public void QuitGame()
	{
		StopMusic ();
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit();
		#endif
	}

	void LoadScene(int scene)
	{
		SceneManager.LoadScene(scene);
	}

	void StopMusic()
	{
		print ("Stops all music");
		FindObjectOfType<SoundManager> ().StopAllMusic ();
	}

	void PauseMusic(){
		FindObjectOfType<SoundManager> ().PauseAll ();
	}

	void ResumeMusic(){
		FindObjectOfType<SoundManager> ().ResumeAll ();
	}

	IEnumerator Fading(bool quit, int mode){

		anim.SetBool ("Fade", true);
		yield return new WaitUntil(()=>black.color.a==1);

		if (quit) {
			QuitGame ();
		} 
		else {
			LoadScene (mode);
		}
	}
}
