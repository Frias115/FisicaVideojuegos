using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveCannonBall : MonoBehaviour {
	public float ExplosionForce = 140000;
	public float Radius = 5f;
	public GameObject ExplosionPrefab;

	void OnCollisionEnter(Collision col) {
		Explode ();
	}

	public void Explode() {
		Instantiate (ExplosionPrefab, transform.position, Quaternion.identity);

		var hits = Physics.OverlapSphere (transform.position, Radius);

		foreach (var hit in hits) {
			var rb = hit.GetComponent<Rigidbody> ();
			if (rb) {
				rb.AddExplosionForce (ExplosionForce, transform.position, Radius);
			}
		}


		// Que desaparezca la bola
		Destroy (gameObject);
	}
}
