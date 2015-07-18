using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
public class CivilianAI : MonoBehaviour {
	private Vector3 oldPos;

	// Use this for initialization
	void Start () {
		oldPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		NavMeshAgent agent = GetComponent<NavMeshAgent>();
		if (agent.velocity.magnitude < 0.01) {
			Vector2 direction = Random.insideUnitCircle * 5;
			agent.destination = new Vector3(transform.position.x + direction.x, 0, transform.position.z + direction.y);
		}

		oldPos = transform.position;
	}	
}
