using UnityEngine;
using System.Collections;

public class granddad : MonoBehaviour {

	public int maxHealth;
	private int Health;
	private bool dead;

	// Use this for initialization
	void Start () {
		dead = false;
	}
	
	// Update is called once per frame
	void Update () {
	//wandering goes here

		if (dead){
			die ()
		}
	}

	void OnTriggerEnter (Collider other)
	{
		if (dead)
			return;
		if (other.tag == "Shot") {
			if (other.transform.parent.gameObject.GetComponent<ShotController>().playerNumber != playerNumber) {
				health -= 1;
				if (health <= 0) {
					dead = true;
				}
				Destroy(other.transform.parent.gameObject);
			}
			
		} else if (other.tag == "Slash") {
			if (other.transform.parent.gameObject.GetComponent<SlashController>().playerNumber != playerNumber) {
				health -= 1;
				if (health <= 0) {
					dead = true;
				}
			}
		} 
	}
	
	void die(){
		gameObject.SetActive (false);

	}
}
