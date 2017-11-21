using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkBehaviour : MonoBehaviour {
    
    public float MaxDistance = 0.5f;
    private float DangerZone;
	public float travelDistance;
    private bool reverse = false;
    public float speed;
	public float chargeSpeed;

    private Vector3 StartPos;
    private Vector3 EndPos;

	private float speedX;
	private float speedY;

	private bool isRight;


    private GameObject shark;
    private GameObject player;
    private uint dangerSoundId = 0;
    private bool inDanger = false;
	private bool isHiding;
	private Animator anim;
	private bool canMove = true;

    // Use this for initialization
    void Start () {

		shark = gameObject;
        player = GameObject.FindGameObjectWithTag("player");
        

        DangerZone = 1.5f*travelDistance;

        StartPos = shark.transform.position;
		print (travelDistance);
		print (StartPos.x + travelDistance);
        EndPos = new Vector3(StartPos.x + travelDistance, StartPos.y, StartPos.z);
		print (EndPos [0]);
		isHiding = false;
		anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update () {

        shark.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);

		if(canMove){

		if (SharkOnPath() && (FindPlayerDistance() > MaxDistance || isHiding))
        {
            float step = speed * Time.deltaTime;

            if (!reverse)
            {
				float Xposition = transform.position [0];
				float Yposition = transform.position [1];
                transform.position = Vector3.MoveTowards(transform.position, EndPos, step);
				anim.SetFloat("speedX", Xposition - transform.position [0]);
				anim.SetFloat("speedY", Yposition - transform.position [1]);

				if (Xposition - transform.position [0] > 0) {
					isRight = true;
				} else {
					isRight = false;
				}

                if(transform.position.x >= EndPos.x)
                {
                    reverse = !reverse;
                }
            }
            else
            {	
				float Xposition = transform.position [0];
				float Yposition = transform.position [1];
                transform.position = Vector3.MoveTowards(transform.position, StartPos, step);
				anim.SetFloat("speedX", Xposition - transform.position [0]);
				anim.SetFloat("speedY", Yposition - transform.position [1]);

				if (Xposition - transform.position [0] > 0) {
					isRight = true;
				} else {
					isRight = false;
				}

                if(transform.position.x <= StartPos.x)
                {
                    reverse = !reverse;
                }
            }

        }
		else if(FindPlayerDistance() < MaxDistance && InDangerZone() && !isHiding)
        {
			float step = chargeSpeed * Time.deltaTime;
			float Xposition = transform.position [0];
			float Yposition = transform.position [1];
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
			anim.SetFloat("speedX", Xposition - transform.position [0]);
			anim.SetFloat("speedY", Yposition - transform.position [1]);
			if (Xposition - transform.position [0] > 0) {
				isRight = true;
			} else {
				isRight = false;
			}
        }
        else
        {
			float Xposition = transform.position [0];
			float Yposition = transform.position [1];
			GoBackOnPath(speed * Time.deltaTime);
			anim.SetFloat("speedX", Xposition - transform.position [0]);
			anim.SetFloat("speedY", Yposition - transform.position [1]);
			if (Xposition - transform.position [0] > 0) {
				isRight = true;
			} else {
				isRight = false;
			}
            reverse = false;
			//print ("Go back");
			}
		}
    }

	public void StopMoving(){
		canMove = false;
	}

    public bool SharkOnPath()
    {
        float start_X = StartPos.x;
        float start_Y = StartPos.y;

        float end_X = EndPos.x;
        float end_Y = EndPos.y;

        float sharkPosX = shark.transform.position.x;
        float sharkPosY = shark.transform.position.y;

		if((sharkPosX >= start_X && sharkPosX <= end_X) && (sharkPosY - 0.1 <= start_Y && sharkPosY + 0.1 >= end_Y))
        {
            return true;
        }

        return false;
    }

    public void GoBackOnPath(float step)
    {
		float antispeed = step/FindDistance (transform.position.x, transform.position.y, StartPos.x, StartPos.y);
		transform.position = Vector3.MoveTowards(transform.position, StartPos, step + antispeed/2);
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

	public float getDirection(){
		if (isRight) {
			return 1;
		}
		return -1;
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
