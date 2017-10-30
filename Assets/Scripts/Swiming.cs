using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Swiming : MonoBehaviour
{

    public float speed = 2f;
    private bool atSurface;
    private bool underWater;

    Rigidbody2D rb2D;
    Collider2D playerCollider;
    Collider2D airPocketCollider;


    // Use this for initialization
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        playerCollider = GameObject.FindGameObjectWithTag("player").GetComponent<Collider2D>();
        airPocketCollider  = GameObject.FindGameObjectWithTag("Air Pocket").GetComponent<Collider2D>();
        

        if (playerCollider.IsTouching(airPocketCollider))
        {
            underWater = false;
        }
        else
        {
            underWater = true;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");


        if (underWater)
        {
            rb2D.velocity = new Vector2(moveHorizontal * speed, moveVertical * speed + 0.5f);
        }
        else
        {
            
        }

    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        atSurface = true;
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Air Pocket"))
        {
            underWater = false;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Air Pocket"))
        {
            underWater = true;
            atSurface = false;
        }
    }
}