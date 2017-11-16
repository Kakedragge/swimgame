﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkBehaviour : MonoBehaviour {


    
    public float MaxDistance = 2;
    private float DangerZone;
    private float travelDistance;
    private bool reverse = false;
    public float speed;

    private Vector3 StartPos;
    private Vector3 EndPos;

    private GameObject shark;
    private GameObject player;
    private uint dangerSoundId = 0;
    private bool inDanger = false;

    // Use this for initialization
    void Start () {

        shark = gameObject;
        player = GameObject.FindGameObjectWithTag("player");
        speed = 1.0f;
        travelDistance = 10.0f;
        DangerZone = 1.5f*travelDistance;

        StartPos = shark.transform.position;
        EndPos = new Vector3(StartPos.x + travelDistance, StartPos.y, StartPos.z);


    }

    // Update is called once per frame
    void Update () {

        shark.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);

        if (SharkOnPath() && (FindPlayerDistance() > MaxDistance))
        {
            float step = speed * Time.deltaTime;

            if (!reverse)
            {
                transform.position = Vector3.MoveTowards(transform.position, EndPos, step);
                if(transform.position.x >= EndPos.x)
                {
                    reverse = !reverse;
                }
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, StartPos, step);
                if(transform.position.x <= StartPos.x)
                {
                    reverse = !reverse;
                }
            }

        }
        else if(FindPlayerDistance() < MaxDistance && InDangerZone())
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
            
        }
        else
        {
            
            GoBackOnPath(speed * Time.deltaTime);
            reverse = false;
        }
    }

    

    public bool SharkOnPath()
    {
        float start_X = StartPos.x;
        float start_Y = StartPos.y;

        float end_X = EndPos.x;
        float end_Y = EndPos.y;

        float sharkPosX = shark.transform.position.x;
        float sharkPosY = shark.transform.position.y;

        if((sharkPosX >= start_X && sharkPosX <= end_X) && (sharkPosY >= start_Y && sharkPosY <= end_Y))
        {
            return true;
        }

        return false;
    }

    public void GoBackOnPath(float step)
    {

        transform.position = Vector3.MoveTowards(transform.position, StartPos, step);
    }

    private float FindDistance(float x1, float y1, float x2, float y2)
    {
        return Mathf.Sqrt(Mathf.Pow(x2 - x1, 2) + Mathf.Pow(y2 - y1, 2));

    }

    public bool InDangerZone()
    {
        
        float pos_X = StartPos.x + ((EndPos.x - StartPos.x)/2);
        float pos_Y = StartPos.y;

        float distance = FindDistance(pos_X, pos_Y, player.transform.position.x, player.transform.position.y);
        
        if(distance > DangerZone)
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
}
