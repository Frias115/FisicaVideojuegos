using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour {
	public bool Grounded;
	public Rigidbody MovingPlatformRb;

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.layer == Layers.Terrain) {
			Grounded = true;
		}

		if (other.gameObject.layer == Layers.MovingPlatforms) {
			MovingPlatformRb = other.GetComponent<Rigidbody> ();
		}

	}

	void OnTriggerStay(Collider other) {
		if (other.gameObject.layer == Layers.Terrain) {
			Grounded = true;
		}
		if (other.gameObject.layer == Layers.MovingPlatforms) {
			MovingPlatformRb = other.GetComponent<Rigidbody> ();
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.gameObject.layer == Layers.Terrain) {
			Grounded = false;
		}
		if (other.gameObject.layer == Layers.MovingPlatforms) {
			MovingPlatformRb = null;
		}

	}

}
