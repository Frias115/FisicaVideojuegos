using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {
	public float TimeToExplode = 3f;
	public float Radius = 2f;
	public float ExplosionForce = 2000;
	public bool Exploded;
	public GameObject ExplosionPrefab;

	// Use this for initialization
	void Start () {
		Invoke ("Explode", TimeToExplode);
		iTween.PunchScale (gameObject, iTween.Hash("amount", Vector3.one * .5f, "looptype", iTween.LoopType.loop, "time", TimeToExplode/5f));
		iTween.ColorTo (gameObject, Color.red, TimeToExplode);
	}

	public void Explode () {
		// Empujar a los objetos cercanos
		Exploded = true;
		var hits = Physics.OverlapSphere (transform.position, Radius);

		foreach (var hit in hits) {
			var otherBomb = hit.GetComponent<Bomb> ();
			if (otherBomb && !otherBomb.Exploded) {
				otherBomb.Explode ();
			} else {
				var rb = hit.GetComponent<Rigidbody> ();
				if (rb) {
					rb.AddExplosionForce (ExplosionForce, transform.position, Radius);
				}
			}
		}

		Instantiate (ExplosionPrefab, transform.position, Quaternion.identity);

		// Que desaparezca la bomba
		Destroy (gameObject);
	}
}
