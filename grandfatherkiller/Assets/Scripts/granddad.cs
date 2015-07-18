using UnityEngine;
using System.Collections;

public class granddad : MonoBehaviour {

	public int maxHealth = 3;
	private int health;
	private bool dead;
	public int grandadNum;

	// Use this for initialization
	void Start () {
		health = maxHealth;
		dead = false;
		PlayerPrefs.SetInt("Winner", 0);
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
				}
				Destroy(other.transform.parent.gameObject);

			
		} else if (other.tag == "Slash") {

				health -= 1;
				if (health <= 0) {
					dead = true;
				}

		} 
	}
	
	void die(){
		gameObject.SetActive (false);
		if (grandadNum == 1) {
			//game over, player 2 wins
			PlayerPrefs.SetInt("Winner", PlayerPrefs.GetInt("Winner")+2);
			Invoke("endGame", 2);
		} else if (grandadNum == 2) {
			//game over, player 1 wins
			PlayerPrefs.SetInt("Winner", PlayerPrefs.GetInt("Winner")+1);
			Invoke("endGame", 2);
		}
	}

	void endGame() {
		Application.LoadLevel("end");
	}
}
