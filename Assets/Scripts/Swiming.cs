using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Swiming : MonoBehaviour
{

    public float speed = 2f;
    private bool inWater;

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
            inWater = false;
        }
        else
        {
            inWater = true;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");


        if (inWater)
        {
            rb2D.velocity = new Vector2(moveHorizontal * speed, moveVertical * speed - 0.5f);
        }
        else
        {
            rb2D.velocity = rb2D.velocity*-10;
        }

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Air Pocket"))
        {
            inWater = false;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Air Pocket"))
        {
            inWater = true;
        }
    }
}