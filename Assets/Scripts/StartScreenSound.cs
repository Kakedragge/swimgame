using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreenSound : MonoBehaviour {

	[FMODUnity.EventRef]
	private string startMusic = "event:/StartScreenMusic";
	FMOD.Studio.ParameterInstance volumeParameter;
	FMOD.Studio.EventInstance musicEv;

	// Use this for initialization
	void Start () {
		musicEv = FMODUnity.RuntimeManager.CreateInstance(startMusic);
		musicEv.getParameter("end", out volumeParameter);

		FMOD.Studio.PLAYBACK_STATE play_state;
		musicEv.getPlaybackState(out play_state);

		if (play_state != FMOD.Studio.PLAYBACK_STATE.PLAYING)
		{
			print("Starts startscreen sound");
			volumeParameter.setValue(1.0f);
			musicEv.start();

		}
	}

	public void StopStartSound(){
		musicEv.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
		musicEv.release();
	}
}
