using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryScreenMusic : MonoBehaviour {

	[FMODUnity.EventRef]
	private string victorySound = "event:/VictoryMusic";
	FMOD.Studio.ParameterInstance stopParameter;
	FMOD.Studio.EventInstance victoryEv;

	// Use this for initialization
	void Start () {

		victoryEv = FMODUnity.RuntimeManager.CreateInstance(victorySound);
		victoryEv.getParameter("end", out stopParameter);

		FMOD.Studio.PLAYBACK_STATE play_state;
		victoryEv.getPlaybackState(out play_state);

		if (play_state != FMOD.Studio.PLAYBACK_STATE.PLAYING)
		{
			stopParameter.setValue (2.0f);	
			victoryEv.start();

		}
	}

	public void StopVictorySound(){
		victoryEv.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
		victoryEv.release();
	}
}
