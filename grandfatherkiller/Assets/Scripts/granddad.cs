using UnityEngine;
using System.Collections;

public class granddad : MonoBehaviour {

	public int maxHealth = 3;
	private int health;
	private bool dead;
	public int grandadNum;
	public GameObject bloodSplatter;

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
			Vector3 bloodSplatterPos = transform.position;
			Quaternion bloodSplatterRotation = Quaternion.Euler(90, Random.Range(0, 360), 0);
			bloodSplatterPos.y = 0.01f;
			GameObject zBlood = (GameObject)Instantiate (bloodSplatter, bloodSplatterPos, bloodSplatterRotation);

			if(other.transform.parent.gameObject.GetComponent<ShotController>().playerNumber == 1)
			{
				PlayerPrefs.SetInt("player1CivKills", PlayerPrefs.GetInt("player1CivKills")+1);
			}
			if(other.transform.parent.gameObject.GetComponent<ShotController>().playerNumber == 2)
			{
				PlayerPrefs.SetInt("player2CivKills", PlayerPrefs.GetInt("player2CivKills")+1);
			}

			if (health <= 0) {
				dead = true;
			}
			Destroy(other.transform.parent.gameObject);	
		} else if (other.tag == "Slash") {
			health -= 1;
			Vector3 bloodSplatterPos = transform.position;
			Quaternion bloodSplatterRotation = Quaternion.Euler(90, Random.Range(0, 360), 0);
			bloodSplatterPos.y = 0.01f;
			GameObject zBlood = (GameObject)Instantiate (bloodSplatter, bloodSplatterPos, bloodSplatterRotation);

			if(other.transform.parent.gameObject.GetComponent<SlashController>().playerNumber == 1)
			{
				PlayerPrefs.SetInt("player1CivKills", PlayerPrefs.GetInt("player1CivKills")+1);
			}
			if(other.transform.parent.gameObject.GetComponent<SlashController>().playerNumber == 2)
			{
				PlayerPrefs.SetInt("player2CivKills", PlayerPrefs.GetInt("player2CivKills")+1);
			}

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
			Invoke("endGame", 0.5f);
		} else if (grandadNum == 2) {
			//game over, player 1 wins
			PlayerPrefs.SetInt("Winner", PlayerPrefs.GetInt("Winner")+1);
			Invoke("endGame", 0.5f);
		}
	}

	void endGame() {
		Application.LoadLevel("end");
	}
}
