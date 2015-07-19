using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
public class PoliceAI : MonoBehaviour {

	public float searchRange = 5.0f;
	public GameObject player1;
	public GameObject player2;
	public GameObject granddad1;
	public GameObject granddad2;

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
		RaycastHit hit;
		float targetDistance = Mathf.Infinity;
		GameObject target = null;
		if (PlayerPrefs.GetInt ("player1CivKills") >= 1) {
			Physics.Raycast(transform.position, player1.transform.position - transform.position, out hit);
			if (hit.collider.gameObject == player1) {
				target = player1;
				targetDistance = hit.distance;
			}
		}
		if (PlayerPrefs.GetInt ("player2CivKills") >= 1) {
			Physics.Raycast(transform.position, player2.transform.position - transform.position, out hit);
			if (hit.collider.gameObject == player2) {
				if (hit.distance < targetDistance) {
					target = player2;
					targetDistance = hit.distance;
				}
			}
		}
		if (target != null) {
			agent.Stop();
			transform.LookAt(target.transform.position);
			
		}
		if (agent.velocity.magnitude < 0.01) {
			Vector3 destination;
			RandomPoint(transform.position, searchRange, out destination);
			agent.destination = destination;
		}
	}	
}
