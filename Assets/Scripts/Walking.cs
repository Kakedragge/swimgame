using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walking : MonoBehaviour {

    private Rigidbody2D rb2D;
    public float speed = 2.0f;
    private Collider2D playerCollider;
	private Animator anim;
	private bool canMove = true;

    // Use this for initialization
    void Start () {

        rb2D = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
		anim = GetComponent<Animator>();

    }

    private bool OnSurface()
    {
        GameObject[] surfaceObjects = GameObject.FindGameObjectsWithTag("Surface"); ;

        foreach (GameObject obj in surfaceObjects)
        {
			print (playerCollider.IsTouching (obj.GetComponent<BoxCollider2D> ()));
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

	public void DisableWalking(){
		canMove = false;
	}

    // Update is called once per frame
    void FixedUpdate () {

		if (canMove) {

		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

			if (moveHorizontal > 0) {
				anim.SetBool ("LookingRight", true);
				anim.SetBool ("Moving", true);
			} else if (moveHorizontal < 0) {
				anim.SetBool ("LookingRight", false);
				anim.SetBool ("Moving", true);
			}

			if (OnSurface ()) {
				FindObjectOfType<HealthBar> ().FullHealthBar ();
				rb2D.gravityScale = 0.5f;
				rb2D.velocity = new Vector2 (moveHorizontal * speed, rb2D.velocity.y);

				if (IsMoving ()) {
					FindObjectOfType<SoundManager> ().UpdateWalking (true);
					anim.SetBool ("Moving", true);
					print ("Is called");
				} else {
					FindObjectOfType<SoundManager> ().UpdateWalking (false);
					anim.SetBool ("Moving", false);
				}
            
			} else {
				FindObjectOfType<SoundManager> ().UpdateWalking (false);
			}
		}
		else{
			anim.SetBool ("Moving", false);
			rb2D.velocity = Vector2.zero;
		}
	}
}
