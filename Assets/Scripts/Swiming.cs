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
    private float timeLeft;

    // Use this for initialization
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        rb2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb2D.interpolation = RigidbodyInterpolation2D.Extrapolate;
        playerCollider = GameObject.FindGameObjectWithTag("player").GetComponent<Collider2D>();
        player = GameObject.FindGameObjectWithTag("player");
        timeLeft = 0;
        modified_speed = speed;

    }

    private bool IsUnderWater()
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

    void Update()
    {

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        if (Input.GetKey("space") && IsUnderWater()) {
            modified_speed = 2 * speed;
        }
        else
        {
            modified_speed = speed;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        //CHECK OUT RAYCASTS

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        if (OnSurface())
        {
            rb2D.velocity = new Vector2(moveHorizontal * modified_speed, 1);
            rb2D.gravityScale = 9.81f;
        }

        else if (IsUnderWater())
        {   
            
            rb2D.velocity = new Vector2(moveHorizontal * modified_speed, moveVertical * modified_speed);
            rb2D.gravityScale = 0;
        }

        else if(InWater() && !IsUnderWater())
        {
            
            
            if (moveVertical > 0)
            {
               rb2D.velocity = new Vector2(moveHorizontal * modified_speed, 0);
            }
            else
            {
               rb2D.velocity = new Vector2(moveHorizontal * modified_speed, moveVertical * modified_speed);
            }
            
            rb2D.gravityScale = 0;
        }
        
        else
        {
            rb2D.gravityScale = 9.81f;
        }
    }
}