using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
public class PoliceAI : MonoBehaviour {

	public float searchRange = 5.0f;
	public GameObject player1;
	public GameObject player2;
	public GameObject granddad1;
	public GameObject granddad2;
	public float fireTime = 3f;
	public Transform shotSpawn;
	public GameObject shot;
	public AudioClip sfxShot;
	public float searchTime = 3.0f;

	private Vector3 lastTarget;
	private float nextAttack;
	private float searchTimeLeft;	

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
		searchTimeLeft = Time.time;
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
		if (target == null) {		
			if (PlayerPrefs.GetInt ("player1CivKills") >= 3) {
				Physics.Raycast(transform.position, granddad1.transform.position - transform.position, out hit);
				if (hit.collider.gameObject == granddad1) {
					target = granddad1;
					targetDistance = hit.distance;
				}
			}
			if (PlayerPrefs.GetInt ("player2CivKills") >= 3) {
				Physics.Raycast(transform.position, granddad2.transform.position - transform.position, out hit);
				if (hit.collider.gameObject == granddad2) {
					if (hit.distance < targetDistance) {
						target = granddad2;
						targetDistance = hit.distance;
					}
				}
			}
		}
		if (target != null) {
			agent.Stop();
			transform.LookAt(target.transform.position);
			if (Time.time > nextAttack)
			{	
				nextAttack = Time.time + fireTime;
				GameObject zBullet = (GameObject)Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
				zBullet.GetComponent<ShotController> ().SetVelocity ();
				zBullet.GetComponent<ShotController> ().playerNumber = 0;
				AudioSource.PlayClipAtPoint (sfxShot, shotSpawn.position, 1.0f);
				lastTarget = target.transform.position;
				searchTimeLeft = Time.time + searchTime;
			}
		} else {
			if (agent.velocity.magnitude < 0.01) {
				Vector3 destination;
				RandomPoint(transform.position, searchRange, out destination);
				agent.destination = destination;
			}
			if (Time.time < searchTimeLeft) {
				agent.destination = lastTarget;
			}
			agent.Resume();
			nextAttack = Time.time + fireTime;
		}
	}	
}
