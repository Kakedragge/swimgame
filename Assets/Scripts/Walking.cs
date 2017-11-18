using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walking : MonoBehaviour {

    private Rigidbody2D rb2D;
    public float speed = 2.0f;
    private Collider2D playerCollider;
	private Animator anim;

    [FMODUnity.EventRef]
    private string mainMusic = "event:/Footsteps";
    FMOD.Studio.ParameterInstance isWalking;
    FMOD.Studio.EventInstance musicEv;


    // Use this for initialization
    void Start () {

        musicEv = FMODUnity.RuntimeManager.CreateInstance(mainMusic);
        musicEv.getParameter("Walking", out isWalking);

        rb2D = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
		anim = GetComponent<Animator>();

        FMOD.Studio.PLAYBACK_STATE play_state;
        musicEv.getPlaybackState(out play_state);

        if (play_state != FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {
            print("Starts regular sound");
            if (OnSurface())
            {
                isWalking.setValue(1.0f);
            }
            else
            {
                isWalking.setValue(0.0f);
            }
            musicEv.start();

        }
    }

    private bool OnSurface()
    {
        GameObject[] surfaceObjects = GameObject.FindGameObjectsWithTag("Surface"); ;

        foreach (GameObject obj in surfaceObjects)
        {
            if (playerCollider.IsTouching(obj.GetComponent<BoxCollider2D>()))
            {
				anim.SetBool("Walking", true);
                return true;
            }
        }
		anim.SetBool("Walking", false);
        return false;
    }

    private bool IsMoving()
    {
        return 0 != Input.GetAxis("Horizontal");
    }

    // Update is called once per frame
    void FixedUpdate () {

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

		if (moveHorizontal > 0) {
			anim.SetBool ("LookingRight", true);
			anim.SetBool ("Moving", true);
		} else if (moveHorizontal < 0) {
			anim.SetBool ("LookingRight", false);
			anim.SetBool ("Moving", true);
		}

        if (OnSurface())
        {
            
            rb2D.gravityScale =  0.5f;
            rb2D.velocity = new Vector2(moveHorizontal * speed, rb2D.velocity.y);

            if (IsMoving())
            {
				anim.SetBool ("Moving", true);
                isWalking.setValue(1.0f);
                print("Is called");
            }
            else
            {
				anim.SetBool ("Moving", false);
                isWalking.setValue(0.0f);
            }
            
        }
        else
        {
            isWalking.setValue(0.0f);
        }
    }
}
