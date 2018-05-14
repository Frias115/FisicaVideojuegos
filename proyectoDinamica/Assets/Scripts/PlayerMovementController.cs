using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MovementController {
	public GameObject BombPrefab;

	protected override Vector2 GetDesiredMovement () {
		return new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical")).normalized;
	}

	protected override void Update() {
		base.Update ();
		if (Input.GetKeyDown (KeyCode.Space) && _groundCheck.Grounded) {
			// Salto
			_rb.AddForce(Vector3.up * JumpForce, ForceMode.VelocityChange);
		}

		if (Input.GetKeyDown (KeyCode.Q)) {
			Instantiate (BombPrefab, transform.position, Quaternion.identity);
		}
	}
}
