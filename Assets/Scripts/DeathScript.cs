using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScript : MonoBehaviour {

    private GameObject player;
    private GameObject shark;

	// Use this for initialization
	void Start () {
        shark = GameObject.FindGameObjectWithTag("Shark");
        player = GameObject.FindGameObjectWithTag("player");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Shark"))
        {
            print("Collided with Shark");
        }
    }
}
