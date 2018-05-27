using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovementController : MonoBehaviour {
	protected Rigidbody _rb;
	protected GroundCheck _groundCheck;
	protected Animator _anim;
	protected Vector2 _input;
	protected Animation _a;

	public float MaxHorizontalSpeed = 5;
	public float Acceleration = 80, Deceleration = 10;
	public float JumpForce = 12;
    public Vector3 forward, right;

    // Use this for initialization
    virtual protected void Awake () {
		_rb = GetComponent<Rigidbody> ();
		_groundCheck = GetComponentInChildren<GroundCheck> ();
		_anim = GetComponentInChildren<Animator> ();
	}

	abstract protected Vector2 GetDesiredMovement ();
    abstract protected Vector2 GetDesiredVelocity();


    // Update is called once per frame
    virtual protected void Update () {
		_input = GetDesiredMovement();
        Vector2 velocity2d = GetDesiredVelocity();

		var currentSpeed = _rb.velocity;
		currentSpeed.y = 0;

		_rb.AddForce (-currentSpeed * Deceleration, ForceMode.VelocityChange);

		if (velocity2d.magnitude > MaxHorizontalSpeed) {
			var auxVector = velocity2d.normalized * MaxHorizontalSpeed;
			_rb.velocity = new Vector3 (auxVector.x, _rb.velocity.y, auxVector.y);
		}

		if (_groundCheck.MovingPlatformRb) {
			_rb.AddForce (_groundCheck.MovingPlatformRb.velocity, ForceMode.VelocityChange);
		}
	}
}