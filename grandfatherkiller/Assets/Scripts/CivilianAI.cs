﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
public class CivilianAI : MonoBehaviour {

	public float searchRange = 5.0f;
	bool RandomPoint(Vector3 center, float range,  out Vector3 result) {
		for (int i = 0; i < 30; i++) {
			Vector3 randomPoint = center + Random.insideUnitSphere * range;
			NavMeshHit hit;
			if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, 1)) {
				result = hit.position;
				return true;
			}
		}
		result = Vector3.zero;
		return false;
	}


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		NavMeshAgent agent = GetComponent<NavMeshAgent>();
		if (agent.velocity.magnitude < 0.01) {
			Vector3 destination;
			RandomPoint(transform.position, searchRange, out destination);
			agent.destination = destination;
		}
	}	
}