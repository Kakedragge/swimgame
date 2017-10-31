using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Swiming : MonoBehaviour
{

    public float speed = 2f;
    private bool onSolidGround;

    private Rigidbody2D rb2D;
    private Collider2D playerCollider;

    // Use this for initialization
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        rb2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb2D.interpolation = RigidbodyInterpolation2D.Extrapolate;
        playerCollider = GameObject.FindGameObjectWithTag("player").GetComponent<Collider2D>();

    }

    private bool IsUnderWater()
     {
        GameObject[] airPocketObjects = GameObject.FindGameObjectsWithTag("AirPockets");
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

    private bool InWater()
        {
        GameObject[] waterObjects = GameObject.FindGameObjectsWithTag("Water");
        foreach (GameObject obj in waterObjects)
        {
            if (playerCollider.IsTouching(obj.GetComponent<Collider2D>()))
            {
                return true;
            }
        }
        return false;
    }

    private bool OnSurface()
    {
        GameObject[] surfaceObjects = GameObject.FindGameObjectsWithTag("Surface"); ;

        foreach (GameObject obj in surfaceObjects)
        {
            if (playerCollider.IsTouching(obj.GetComponent<Collider2D>()))
            {
                return true;
            }
        }
        return false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        
    }
}