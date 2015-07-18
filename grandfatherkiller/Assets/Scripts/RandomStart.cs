using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
public class RandomStart : MonoBehaviour {

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
		Vector3 startPos;
		for (int i = 0; i < 10; i++) {
			RandomPoint(transform.position, 10.0f, out startPos);
			transform.position = startPos;
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
