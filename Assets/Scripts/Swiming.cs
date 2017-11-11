﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Swiming : MonoBehaviour
{

    public float speed = 2f;
    private bool onSolidGround;
    private float modified_speed;
    private Rigidbody2D rb2D;
    private Collider2D playerCollider;
    private GameObject player;
    private GameObject shark;
    private float timeLeft;
    private bool isUnderWater;
    private bool isOnSurface;
    private bool isAtWaterSurface;
    private uint playingMusicId = 0;
    private int MaxDistance = 2;
    private float DangerZone = 7.5f;
    private float swimmingValue = 1.0f;

    //Player location states

    [FMODUnity.EventRef]
    public string dangerMusic = "event:/Main";
    public string swimSound = "event:/Swim";
    FMOD.Studio.EventInstance musicEv;
    FMOD.Studio.EventInstance swimEv;
    FMOD.Studio.ParameterInstance placingParameter;
    FMOD.Studio.ParameterInstance muteParameter;
    FMOD.Studio.ParameterInstance isSwimming;

    // Use this for initialization
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        rb2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb2D.interpolation = RigidbodyInterpolation2D.Extrapolate;
        playerCollider = GameObject.FindGameObjectWithTag("player").GetComponent<Collider2D>();
        player = GameObject.FindGameObjectWithTag("player");
        shark = GameObject.FindGameObjectWithTag("Shark");
        timeLeft = 0;
        modified_speed = speed;

        musicEv = FMODUnity.RuntimeManager.CreateInstance(dangerMusic);
        swimEv = FMODUnity.RuntimeManager.CreateInstance(swimSound);

        musicEv.getParameter("Underwater", out placingParameter);
        musicEv.getParameter("Shark", out muteParameter);

        swimEv.getParameter("IsSwimming", out isSwimming);

        FMOD.Studio.PLAYBACK_STATE play_state;
        musicEv.getPlaybackState(out play_state);
        if (play_state != FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {
            muteParameter.setValue(InDanger());
            placingParameter.setValue(FindDepth());
            musicEv.start();

        }


    }

    private float FindDepth()
    {
        float your_pos = transform.position.y;
        float minPos = -6.9f;
        float maxPos = -21.5f;

        float depth = maxPos - minPos;

        float percent = (your_pos - minPos) / depth;

        if(percent < 0)
        {
            return 0;
        }
        else if(percent > 1)
        {
            return 1;
        }

        return percent;

    }

    public bool InDangerZone()
    {
        Vector3 StartPos = shark.transform.position;
        Vector3 EndPos = new Vector3(StartPos.x + 5.0f, StartPos.y, StartPos.z);


        float start_X = StartPos.x;
        float end_X = EndPos.x;

        float pos_X = start_X + (end_X - start_X) / 2;
        float pos_Y = player.transform.position.y;

        float distance = FindDistance(pos_X, pos_Y, player.transform.position.x, player.transform.position.y);

        if (distance > DangerZone)
        {
            return false;
        }
        return true;

    }

    public float FindPlayerDistance()
    {
        float start_X = player.transform.position.x;
        float start_Y = player.transform.position.y;

        float end_X = shark.transform.position.x;
        float end_Y = shark.transform.position.y;

        float distance = FindDistance(start_X, start_Y, end_X, end_Y);

        return distance;
    }

    private float FindDistance(float x1, float y1, float x2, float y2)
    {
        return Mathf.Sqrt(Mathf.Pow(x2 - x1, 2) + Mathf.Pow(y2 - y1, 2));

    }

    public float InDanger()
    {
        if(FindPlayerDistance() < MaxDistance && InDangerZone())
        {
            return 1.0f;
        }
        return 0;
    }

    private void UpdateSoundState(bool isMoving)
    {

        FMOD.Studio.PLAYBACK_STATE play_state;
        musicEv.getPlaybackState(out play_state);

        if (play_state != FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {
            placingParameter.setValue(FindDepth());
            muteParameter.setValue(InDanger());
            musicEv.start();

        }
        else
        {
            muteParameter.setValue(InDanger());
            placingParameter.setValue(FindDepth());
        }

        FMOD.Studio.PLAYBACK_STATE play_state1;
        swimEv.getPlaybackState(out play_state1);

        if (play_state1 != FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {

            if (!isMoving)
            {
                isSwimming.setValue(0);
            }
            else
            {
                isSwimming.setValue(swimmingValue);
            }

            swimEv.start();

        }
        else
        {
            if (!isMoving)
            {
                isSwimming.setValue(0);
            }
            else
            {
                isSwimming.setValue(swimmingValue);
            }
        }
    }

    private bool IsUnderWater()
     {
        GameObject[] airPocketObjects = GameObject.FindGameObjectsWithTag("Air Pocket");
        foreach (GameObject obj in airPocketObjects)
        {
            if (playerCollider.IsTouching(obj.GetComponent<Collider2D>()))
            {
                return false;
            }
        }
        if (InWater())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool StandingOnWater(GameObject player, GameObject waterObject)
    {


        if (player.transform.position.y > (waterObject.GetComponent<BoxCollider2D>().bounds.max.y - 0.03))
        {
            
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool InWater()
        {
        GameObject[] waterObjects = GameObject.FindGameObjectsWithTag("Water");
        foreach (GameObject obj in waterObjects)
        {
            if (playerCollider.IsTouching(obj.GetComponent<Collider2D>()))
            {
                if (!StandingOnWater(GameObject.FindGameObjectWithTag("player"), obj)){
                    return true;
                }
            }
        }
        return false;
    }

    private bool OnSurface()
    {
        GameObject[] surfaceObjects = GameObject.FindGameObjectsWithTag("Surface"); ;

        foreach (GameObject obj in surfaceObjects)
        {
            if (playerCollider.IsTouching(obj.GetComponent<BoxCollider2D>()))
            {
                return true;
            }
        }
        return false;
    }

    void Update()
    {
        

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        UpdateSoundState(moveHorizontal != 0 || moveVertical != 0);

        if (Input.GetKey("space") && IsUnderWater()) {
            modified_speed = 2 * speed;
            swimmingValue = 0.5f;
        }
        else
        {
            modified_speed = speed;
            swimmingValue = 1.0f;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        //CHECK OUT RAYCASTS

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        if (OnSurface())
        {
            if (!isOnSurface)
            {
                rb2D.gravityScale = 9.81f;
            }
            rb2D.velocity = new Vector2(moveHorizontal * modified_speed, 1);
        }

        else if (IsUnderWater())
        {   
            
            if (!isUnderWater)
            {
                rb2D.gravityScale = 0;
            }
            rb2D.velocity = new Vector2(moveHorizontal * modified_speed, moveVertical * modified_speed);
        }

        else if(InWater() && !IsUnderWater())
        {

            if (!isAtWaterSurface)
            {
                rb2D.gravityScale = 0;
            }

            if (moveVertical > 0)
            {
               rb2D.velocity = new Vector2(moveHorizontal * modified_speed, 0);
            }
            else
            {
               rb2D.velocity = new Vector2(moveHorizontal * modified_speed, moveVertical * modified_speed);
            }
            
        }
        
        else
        {
            rb2D.gravityScale = 9.81f;
        }
    }
}