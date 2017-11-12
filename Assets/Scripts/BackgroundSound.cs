using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSound : MonoBehaviour {

    // Use this for initialization

    [FMODUnity.EventRef]
    private string mainMusic = "event:/Main";
    FMOD.Studio.ParameterInstance placingParameter;
    FMOD.Studio.ParameterInstance muteParameter;
    FMOD.Studio.EventInstance musicEv;

    private string dangerMusic = "event:/DangerMusic";
    FMOD.Studio.EventInstance dangerEv;
    FMOD.Studio.ParameterInstance volumeParameter;

    private GameObject player;
    private GameObject shark;
    private int MaxDistance = 2;


    void Start () {

        dangerEv = FMODUnity.RuntimeManager.CreateInstance(dangerMusic);
        dangerEv.getParameter("end", out volumeParameter);

        musicEv = FMODUnity.RuntimeManager.CreateInstance(mainMusic);
        musicEv.getParameter("Underwater", out placingParameter);
        musicEv.getParameter("Shark", out muteParameter);

        player = GameObject.FindGameObjectWithTag("player");
        shark = GameObject.FindGameObjectWithTag("Shark");

        FMOD.Studio.PLAYBACK_STATE play_state1;
        dangerEv.getPlaybackState(out play_state1);

        FMOD.Studio.PLAYBACK_STATE play_state;
        musicEv.getPlaybackState(out play_state);

        if (play_state != FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {
            print("Starts regular sound");
            muteParameter.setValue(InDanger());
            placingParameter.setValue(FindDepth());
            musicEv.start();

        }

        if (play_state1 != FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {
            print("Starts danger sound");
            volumeParameter.setValue(FindPlayerDistance());
            dangerEv.start();

        }
    }

    private float FindDistance(float x1, float y1, float x2, float y2)
    {
        return Mathf.Sqrt(Mathf.Pow(x2 - x1, 2) + Mathf.Pow(y2 - y1, 2));

    }



    public float FindPlayerDistance()
    {
        float start_X = player.transform.position.x;
        float start_Y = player.transform.position.y;

        float end_X = shark.transform.position.x;
        float end_Y = shark.transform.position.y;

        float distance = FindDistance(start_X, start_Y, end_X, end_Y);

        if(distance > 2.0f)
        {
            return 2.0f;
        }
        else if(distance < 0.0f)
        {
            return 0.0f;
        }

        return distance;
    }

    public float InDanger()
    {
        if (FindPlayerDistance() < MaxDistance && GetComponent<Swiming>().InDangerZone())
        {
            return 1.0f;
        }
        return 0.0f;
    }

    private float FindDepth()
    {
        float your_pos = transform.position.y;
        float minPos = -6.9f;
        float maxPos = -21.5f;

        float depth = maxPos - minPos;

        float percent = (your_pos - minPos) / depth;

        if (percent < 0)
        {
            return 0;
        }
        else if (percent > 1)
        {
            return 1;
        }

        return percent;

    }

        
    

    // Update is called once per frame
    void Update () {


        placingParameter.setValue(FindDepth());
        volumeParameter.setValue(FindPlayerDistance());
        muteParameter.setValue(InDanger());
        
    }
}
