using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAgent : MonoBehaviour {
	protected NavMeshAgent _agent;

	// Use this for initialization
	void Awake () {
		_agent = GetComponent<NavMeshAgent> ();

		_agent.destination = new Vector3 (310, 3, 196);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
