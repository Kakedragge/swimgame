using System.Collections;
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
	private bool isHiding;
	private Animator anim;

    // Use this for initialization
    void Start () {

        shark = GameObject.FindGameObjectWithTag("Shark");
        player = GameObject.FindGameObjectWithTag("player");
        speed = 1.0f;
        travelDistance = 5.0f;
        DangerZone = travelDistance*1.5f;

        StartPos = shark.transform.position;
        EndPos = new Vector3(StartPos.x + travelDistance, StartPos.y, StartPos.z);
		isHiding = false;
		anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update () {

        shark.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);

		if (SharkOnPath() && (FindPlayerDistance() > MaxDistance) && isHiding)
        {
            float step = speed * Time.deltaTime;

            if (!reverse)
            {
                transform.position = Vector3.MoveTowards(transform.position, EndPos, step);
                if(transform.position.x >= EndPos.x)
                {
                    reverse = !reverse;
					anim.SetBool("SwimLeft", true);
                }
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, StartPos, step);
                if(transform.position.x <= StartPos.x)
                {
                    reverse = !reverse;
					anim.SetBool("SwimLeft", false);
                }
            }

        }
		else if(FindPlayerDistance() < MaxDistance && InDangerZone() && !isHiding)
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
        float start_X = StartPos.x;
        float end_X = EndPos.x;

        float pos_X = start_X + (end_X - start_X) / 2;
        float pos_Y = player.transform.position.y;

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

	public void setIsHiding(bool hiding) {
		isHiding = hiding;

	}

}
