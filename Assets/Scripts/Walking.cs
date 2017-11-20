using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walking : MonoBehaviour {

    private Rigidbody2D rb2D;
    public float speed = 2.0f;
    private Collider2D playerCollider;

    // Use this for initialization
    void Start () {
        rb2D = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();        
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

    private bool IsMoving()
    {
        return 0 != Input.GetAxis("Horizontal");
    }

    // Update is called once per frame
    void Update () {

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        if (OnSurface())
        {
            
            rb2D.gravityScale =  1.0f;
            rb2D.velocity = new Vector2(moveHorizontal * speed, rb2D.velocity.y);

            if (IsMoving())
            {
				FindObjectOfType<SoundManager> ().UpdateWalking (true);
                print("Is called");
            }
            else
            {
				FindObjectOfType<SoundManager> ().UpdateWalking (false);
            }
            
        }
        else
        {
			FindObjectOfType<SoundManager> ().UpdateWalking (false);
        }
    }
}
