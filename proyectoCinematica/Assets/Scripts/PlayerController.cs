using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MovementController {

	public GameObject DaggerPrefab;
	public float throwVelocity = 6;

	//https://pastebin.com/14H2X5j7

	// Use this for initialization
	protected override void Awake () {
		base.Awake ();
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update ();

		if (Input.GetKeyDown (KeyCode.Space)) {
			TryJump ();
		} else if (Input.GetKeyUp(KeyCode.Space)){
			// Calcular cuánto tiempo he apretado el botón
			var timePressed = Time.time - _jumpPressTime;
			// Calcular cuánto tengo que saltar en función a ese tiempo
			var percentHeight = Mathf.Clamp(timePressed/FullJumpTime, MinJumpPercent, 1);
			// Recalcular _targetHeight en función a esa "cantidad" de salto
			targetHeight = _jumpStartY + JumpHeight * percentHeight;
			Debug.Log ("Jumping " + (JumpHeight * percentHeight) +" meters");
		}

		//Disparo
		if (Input.GetMouseButtonDown(0)) {
			Vector3 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
			Vector2 offs = new Vector2(direction.x, direction.y);
            float angl = Mathf.Atan2(offs.y, offs.x) * Mathf.Rad2Deg;
			if(angl > 90 ){
				angl = angl -180;
			} else if(angl < -90){
				angl = angl +180;
			}
			GameObject dagger = GameObject.Instantiate(DaggerPrefab, transform.position, Quaternion.Euler(0, 0, angl));
			dagger.GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x, direction.y).normalized * throwVelocity;    
		}

		//Correr
		if(Input.GetKey(KeyCode.LeftShift)){  
			sprinting = true;
		}
		else{
			sprinting = false;
		} 

		//Agacharse
		if(Input.GetKey(KeyCode.S)){
			crouching = true;
		} 
		else{
			crouching = false;
		} 

	}

	float AngleBetweenTwoPoints(Vector3 a, Vector3 b) {
       	return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

	protected override void DetermineDirection() {
		h = Input.GetAxisRaw ("Horizontal");
	}
}