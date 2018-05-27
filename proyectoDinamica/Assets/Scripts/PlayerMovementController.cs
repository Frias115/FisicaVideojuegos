using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MovementController {
	public GameObject BombPrefab;
    public float fastJumpTimeThreshold= 1;
    [Range(0.0f, 1.0f)]
    public float jumpAcceleration;
    [Range(0.0f, 1.0f)]
    public float jumpDeceleration;
    public float maxShootForce;
    public float ShootPressTimeThreshold = 0.5f;


    private float shootPressTimer = 0;
    private float _maxShootForce;
    private Vector3 vectorForward;
    private float fastJumpTimer;
    private int numberOfJumps = 0;
    private float baseAcceleration, baseDeceleration;

    protected override void Awake()
    {
        base.Awake();
        fastJumpTimer = fastJumpTimeThreshold;
        baseAcceleration = Acceleration;
        baseDeceleration = Deceleration;
        _maxShootForce = maxShootForce;
    }

    protected override Vector2 GetDesiredMovement () {
		return new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical")).normalized;
	}

    protected override Vector2 GetDesiredVelocity()
    {
        forward = Vector3.ProjectOnPlane(Camera.main.transform.forward, transform.up).normalized;
        right = Vector3.ProjectOnPlane(Camera.main.transform.right, transform.up).normalized;

        Vector3 movement = _input.y * forward * Acceleration + _input.x * right * Acceleration;
        _rb.AddForce(movement, ForceMode.VelocityChange);
        Quaternion direction = Quaternion.LookRotation(movement);
        transform.rotation = Quaternion.Lerp(transform.rotation, direction, 100 * Time.deltaTime);

        return new Vector2(_rb.velocity.x, _rb.velocity.z);
    }

    protected override void Update() {
		base.Update ();

        Acceleration = baseAcceleration;
        Deceleration = baseDeceleration;

        if (Input.GetKeyDown (KeyCode.Space) && _groundCheck.Grounded) {
            // Salto
            numberOfJumps = 1;
			_rb.AddForce(Vector3.up * JumpForce, ForceMode.VelocityChange);
		} else if (Input.GetKeyDown(KeyCode.Space) && !_groundCheck.Grounded)
        {
            //Reseteo el timer cada vez que presiono salto en el aire
            fastJumpTimer = 0;
        } else if (_groundCheck.Grounded && fastJumpTimer < fastJumpTimeThreshold)
        {
            // Si ha presionado salto mientras estaba en el aire hace poco tiempo salto directamente
            _rb.AddForce(Vector3.up * (JumpForce + (numberOfJumps * 3)) , ForceMode.VelocityChange);
            // Si salta 3 veces reseteo el numero de saltos
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

        if (!_groundCheck.Grounded)
        {
            // Cambio aceleracion y deceleracion en el aire
            Acceleration *= jumpAcceleration;
            Deceleration *= jumpDeceleration;
        }

        // Disparo segun el tiempo presionado
        if (Input.GetKey(KeyCode.Q))
        {
            shootPressTimer += Time.deltaTime;
        }
        else if (Input.GetKeyUp(KeyCode.Q))
        {
            var percentStrength = Mathf.Clamp(shootPressTimer, ShootPressTimeThreshold, 1);
            maxShootForce *= percentStrength;
            Shoot();
        }
    }

    protected void Shoot()
    {
        var go = Instantiate(BombPrefab, transform.position, Quaternion.identity);
        go.GetComponent<Rigidbody>().AddForce(transform.forward * maxShootForce, ForceMode.VelocityChange);

        ShootPressTimeThreshold = 0;
        maxShootForce = _maxShootForce;
    }

    protected void LateUpdate()
    {
        _anim.SetFloat("MoveSpeed", _input.magnitude);
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
