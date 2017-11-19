using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreenSound : MonoBehaviour {

	[FMODUnity.EventRef]
	private string mainMusic = "event:/Main";
	FMOD.Studio.ParameterInstance placingParameter;
	FMOD.Studio.EventInstance musicEv;

	// Use this for initialization
	void Start () {
		musicEv = FMODUnity.RuntimeManager.CreateInstance(mainMusic);
		musicEv.getParameter("Underwater", out placingParameter);

		FMOD.Studio.PLAYBACK_STATE play_state;
		musicEv.getPlaybackState(out play_state);

		if (play_state != FMOD.Studio.PLAYBACK_STATE.PLAYING)
		{
			print("Starts regular sound");
			placingParameter.setValue(0.0f);
			musicEv.start();

		}
	}
}
