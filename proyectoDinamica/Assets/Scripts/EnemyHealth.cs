using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == Layers.Player)
        {
            other.GetComponentInParent<PlayerMovementController>().Bounce();
            Destroy(GetComponentInParent<EnemyMovementController>().gameObject);
        }
    }
}
