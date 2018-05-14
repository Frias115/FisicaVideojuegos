using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
	public Vector3 Offset;
	public Transform Target;
	public LayerMask LayersToAvoid;

	// Update is called once per frame
	void LateUpdate () {
		transform.position = Target.position + Offset;
		transform.LookAt (Target);

		RaycastHit hit;

		if (Physics.Raycast (Target.position, -transform.forward, out hit, Vector3.Distance(Target.position, transform.position), LayersToAvoid)) {
			// El rayo ha dado a algún objeto
			transform.position = hit.point;
		}
	}
}
