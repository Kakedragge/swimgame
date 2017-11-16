using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionScript : MonoBehaviour {
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Shark")
            FindObjectOfType<GameManager>().EndGame();
    }
}
