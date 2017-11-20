using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour {

	public void LoadSceneByIndex(int index)
    {
		FindObjectOfType<StartScreenSound> ().StopStartSound ();
        SceneManager.LoadScene(index);
    }
}
