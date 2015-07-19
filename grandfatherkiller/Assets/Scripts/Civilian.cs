using UnityEngine;
using System.Collections;

public class Civilian : MonoBehaviour {

	public int maxHealth = 1;
	private int health;
	private bool dead;

	// Use this for initialization
	void Start () {
		health = maxHealth;
		dead = false;
	}
	
	// Update is called once per frame
	void Update () {
	//wandering goes here

		if (dead){
			die ();
		}
	}

	void OnTriggerEnter (Collider other)
	{
		if (dead)
			return;
		if (other.tag == "Shot") {

				health -= 1;
				if (health <= 0) {
					dead = true;

				if(other.transform.parent.gameObject.GetComponent<ShotController>().playerNumber == 1)
				{
					PlayerPrefs.SetInt("player1CivKills", PlayerPrefs.GetInt("player1CivKills")+1);
				}
				if(other.transform.parent.gameObject.GetComponent<ShotController>().playerNumber == 2)
				{
					PlayerPrefs.SetInt("player2CivKills", PlayerPrefs.GetInt("player2CivKills")+1);
				}
				}
				Destroy(other.transform.parent.gameObject);

			
		} else if (other.tag == "Slash") {
				


				health -= 1;
				if (health <= 0) {
					dead = true;

				if(other.transform.parent.gameObject.GetComponent<SlashController>().playerNumber == 1)
				{
					PlayerPrefs.SetInt("player1CivKills", PlayerPrefs.GetInt("player1CivKills")+1);
				}
				if(other.transform.parent.gameObject.GetComponent<SlashController>().playerNumber == 2)
				{
					PlayerPrefs.SetInt("player2CivKills", PlayerPrefs.GetInt("player2CivKills")+1);
				}
				}

		} 
	}
	
	void die() {
		Destroy(gameObject);
	}
}