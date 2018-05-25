using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrictionPlatform : MonoBehaviour {

    [Range(0.01f, 1.0f)]
    public float friction;

    private float playerBaseAcceleration = -1, playerBaseDeceleration = -1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == Layers.Player)
        {
            if(playerBaseAcceleration == -1)
            {
                playerBaseAcceleration = other.GetComponentInParent<PlayerMovementController>().GetbaseAcceleration();
            }

            if (playerBaseDeceleration == -1)
            {
                playerBaseDeceleration = other.GetComponentInParent<PlayerMovementController>().GetbaseDeceleration();
            }

            other.GetComponentInParent<PlayerMovementController>().Acceleration = playerBaseAcceleration * friction;
            other.GetComponentInParent<PlayerMovementController>().Deceleration = playerBaseDeceleration * friction;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == Layers.Player)
        {
            other.GetComponentInParent<PlayerMovementController>().Acceleration = playerBaseAcceleration * friction;
            other.GetComponentInParent<PlayerMovementController>().Deceleration = playerBaseDeceleration * friction;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == Layers.Player)
        {
            other.GetComponentInParent<PlayerMovementController>().Acceleration = playerBaseAcceleration;
            other.GetComponentInParent<PlayerMovementController>().Deceleration = playerBaseDeceleration;
        }
    }
}
