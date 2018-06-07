using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour {

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == Layers.Player)
        {
            other.GetComponentInParent<PlayerHealth>().Damage(1);
        }
    }
}
