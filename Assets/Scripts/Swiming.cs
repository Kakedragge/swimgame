using System.Collections;
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
    private float DangerZone = 7.5f;
    private float swimmingValue = 1.0f;
	private bool stunned = false;
    private Animator anim;
	private bool canMove = true;
    
	//Player location states

	private int state;
	private const int IN_WATER = 0;
	private const int UNDER_WATER = 1;
	private const int ON_SURFACE = 2;

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
        anim = GetComponent<Animator>();

		if (IsUnderWater ()) {
			state = UNDER_WATER;
		} else if (InWater ()) {
			state = IN_WATER;
		} else {
			state = ON_SURFACE;
		}
    }

	private void FackYou() {
		print ("Fack U");
	}

	public void DisableSwimming(){
		canMove = false;
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

    

    private float FindDistance(float x1, float y1, float x2, float y2)
    {
        return Mathf.Sqrt(Mathf.Pow(x2 - x1, 2) + Mathf.Pow(y2 - y1, 2));

    }


    public bool IsUnderWater()
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

	public void setStunned (bool isStunned) {
		stunned = isStunned;
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

    void FixedUpdate()
    {

        //CHECK OUT RAYCASTS

		if (canMove) {

			float moveHorizontal = Input.GetAxis ("Horizontal");
			float moveVertical = Input.GetAxis ("Vertical");

			anim.SetFloat ("SwimX", moveHorizontal);
			anim.SetFloat ("SwimY", moveVertical);
			if (moveHorizontal > 0) {
				anim.SetBool ("LookingRight", true);
			} else if (moveHorizontal < 0) {
				anim.SetBool ("LookingRight", false);
			}

			if (moveHorizontal != 0 || moveVertical != 0) {
				anim.SetBool ("Swimming", true);
				anim.SetBool ("Moving", true);
			} else {
				anim.SetBool ("Swimming", false);
				anim.SetBool ("Moving", false);
			}

        
			if (Input.GetKey (KeyCode.LeftShift) && IsUnderWater ()) {
				modified_speed = 1.8f * speed;
				swimmingValue = 0.5f;
				FindObjectOfType<AirManagement> ().setStaminaMulti (3);
			} else {
				modified_speed = speed;
				swimmingValue = 1.0f;
				FindObjectOfType<AirManagement> ().setStaminaMulti (1);
			}

			if (IsUnderWater ()) {   
				if (state == IN_WATER) {
					FindObjectOfType<SoundManager> ().PlayBreachSplash ();
				}

				state = UNDER_WATER;

				if (!isUnderWater) {
					rb2D.gravityScale = 0;
				}
				if (!stunned) {
					rb2D.velocity = new Vector2 (moveHorizontal * modified_speed, moveVertical * modified_speed);
				}
			} else if (InWater () && !IsUnderWater ()) {

				if (state == UNDER_WATER) {
					//FindObjectOfType<SoundManager> ().PlayBreachSplash ();
					FindObjectOfType<SoundManager> ().PlayExhale ();

				} else if (state == ON_SURFACE) {
					FindObjectOfType<SoundManager> ().PlayFallSplash ();
				}

				state = IN_WATER;

				if (!isAtWaterSurface) {
					rb2D.gravityScale = 0;
				}

				if (!stunned) {
					if (moveVertical > 0) {
						rb2D.velocity = new Vector2 (moveHorizontal * modified_speed, 0);
					} else {
						rb2D.velocity = new Vector2 (moveHorizontal * modified_speed, moveVertical * modified_speed);
					}
				}
            
			} else {
				if (state == IN_WATER) {
					FindObjectOfType<SoundManager> ().PlayBreachSplash ();
				}

				state = ON_SURFACE;

				rb2D.gravityScale = 1.0f;
			}
			FindObjectOfType<SoundManager> ().UpdateSwimming (moveHorizontal != 0 || moveVertical != 0, InWater (), swimmingValue);
			FindObjectOfType<SoundManager> ().UpdateMusic ();

		} else {
			anim.SetBool ("Moving", false);
			rb2D.velocity = Vector2.zero;
		}
    }

}