using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MovementController {
	public GameObject BombPrefab;
    public float fastJumpTimeThreshold= 1;

    private float fastJumpTimer;
    private int numberOfJumps = 0;
    private float baseAcceleration, baseDeceleration;


    public void Start()
    {
        fastJumpTimer = fastJumpTimeThreshold;
        baseAcceleration = Acceleration;
        baseDeceleration = Deceleration;
    }

    protected override Vector2 GetDesiredMovement () {
		return new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical")).normalized;
	}

	protected override void Update() {
		base.Update ();
		if (Input.GetKeyDown (KeyCode.Space) && _groundCheck.Grounded) {
            // Salto
            //Debug.Log("Salto normal");
            numberOfJumps = 1;
			_rb.AddForce(Vector3.up * JumpForce, ForceMode.VelocityChange);
		} else if (Input.GetKeyDown(KeyCode.Space) && !_groundCheck.Grounded)
        {
            fastJumpTimer = 0;
        } else if (_groundCheck.Grounded && fastJumpTimer < fastJumpTimeThreshold)
        {
            //Debug.Log("Salto rapido");
            _rb.AddForce(Vector3.up * (JumpForce + (numberOfJumps * 3)) , ForceMode.VelocityChange);
            //Debug.Log("Numero de salto: " + numberOfJumps);
            if (numberOfJumps == 3)
            {
                numberOfJumps = 1;
            }
            else
            {
                numberOfJumps++;

            }
            fastJumpTimer = fastJumpTimeThreshold;
        }

        fastJumpTimer += Time.deltaTime;

		if (Input.GetKeyDown (KeyCode.Q)) {
			Instantiate (BombPrefab, transform.position, Quaternion.identity);
		}
	}

    public void Bounce()
    {
        _rb.AddForce(Vector3.up * JumpForce, ForceMode.VelocityChange);
    }

    public float GetbaseAcceleration()
    {
        return baseAcceleration;
    }

    public float GetbaseDeceleration()
    {
        return baseDeceleration;
    }
}
