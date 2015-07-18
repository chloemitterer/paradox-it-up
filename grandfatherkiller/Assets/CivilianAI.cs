using UnityEngine;
using System.Collections;

public class CivilianAI : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		NavMeshAgent agent = GetComponent<NavMeshAgent>();
          
		if (!agent.hasPath) {
			Vector2 direction = Random.insideUnitCircle * 5;
			agent.destination = new Vector3(transform.position.x + direction.x, 0, transform.position.z + direction.y);
		}
	}	
}
