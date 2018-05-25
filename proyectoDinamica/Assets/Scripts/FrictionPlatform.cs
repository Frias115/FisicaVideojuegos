using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrictionPlatform : MonoBehaviour {

    [Range(0.01f, 1.0f)]
    public float friction;

    private float playerBaseAcceleration = -1, playerBaseDeceleration = -1;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == Layers.Player)
        {
            if(playerBaseAcceleration == -1)
            {
                playerBaseAcceleration = other.GetComponent<PlayerMovementController>().GetbaseAcceleration();
            }

            if (playerBaseDeceleration == -1)
            {
                playerBaseDeceleration = other.GetComponent<PlayerMovementController>().GetbaseDeceleration();
            }

            other.GetComponent<PlayerMovementController>().Acceleration = playerBaseAcceleration * friction;
            other.GetComponent<PlayerMovementController>().Deceleration = playerBaseDeceleration * friction;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == Layers.Player)
        {
            other.GetComponent<PlayerMovementController>().Acceleration = playerBaseAcceleration * friction;
            other.GetComponent<PlayerMovementController>().Deceleration = playerBaseDeceleration * friction;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == Layers.Player)
        {
            other.GetComponent<PlayerMovementController>().Acceleration = playerBaseAcceleration;
            other.GetComponent<PlayerMovementController>().Deceleration = playerBaseDeceleration;
        }
    }
}
