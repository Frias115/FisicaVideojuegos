using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovementController : MovementController {
	public bool _playerDetected;
	public float SightRange = 10;
	public float StopDistance = 1;
	public float SightAngle = 160;

	protected PlayerMovementController _player;
	protected int _currentCornerIndex;
	protected float _repathRate = 1f;
	protected float _lastRepath = Mathf.NegativeInfinity;
	protected NavMeshPath _path;

	// Use this for initialization
	override protected void Awake () {
		base.Awake ();
		_player = FindObjectOfType<PlayerMovementController> ();
		_path = new NavMeshPath();
	}

	protected bool CheckPlayerVisibility() {
		RaycastHit hit;
		if (Physics.Raycast (transform.position + Vector3.up, _player.transform.position - transform.position, out hit)) {
			if (hit.collider.gameObject.layer == Layers.Player && hit.distance <= SightRange) {
				if (Vector3.Angle(transform.forward, _player.transform.position - transform.position) < SightAngle/2) {
					return true;
				}
			}
		}
		return false;
	}

	protected override Vector2 GetDesiredMovement () {
		_playerDetected = CheckPlayerVisibility ();

		if (_playerDetected && Time.time > _lastRepath + _repathRate) {
			// Calcular camino hasta el jugador
			_lastRepath = Time.time;
			NavMesh.CalculatePath(transform.position, _player.transform.position, NavMesh.AllAreas, _path);
			_currentCornerIndex = 0;
		}

		if (_path.status == NavMeshPathStatus.PathComplete) {
			if (Vector3.Distance (transform.position, _path.corners [_currentCornerIndex]) < StopDistance) {
				if (_currentCornerIndex < _path.corners.Length - 1) {
					_currentCornerIndex++;
				}
			} else {
				return new Vector2 (_path.corners [_currentCornerIndex].x - transform.position.x, _path.corners [_currentCornerIndex].z - transform.position.z).normalized;	
			}
		}


		return Vector2.zero;
	}

	void OnDrawGizmos() {
		if (_path != null) {
			foreach (var corner in _path.corners) {
				Gizmos.DrawWireSphere (corner, .5f);
			}
		}
	}
}