using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour {
	public float Freq = 1;
	public float Force = 30;
	public GameObject BallPrefab;
	public Transform CannonTransform;

	// Use this for initialization
	void Start () {
		StartCoroutine (ShootCoroutine());
	}

	IEnumerator ShootCoroutine() {
		while (true) {
			yield return new WaitForSeconds (Freq);
			var go = Instantiate (BallPrefab, transform.position + Vector3.up * 1.2f + CannonTransform.forward * 1.685f, Quaternion.identity);
			go.GetComponent<Rigidbody> ().AddForce (CannonTransform.forward * Force, ForceMode.VelocityChange);
		}
	}
}
