using UnityEngine;
using System.Collections;

public class SlashController : MonoBehaviour {

	public float slashSpeed;
	public float lifeTime;
	public int playerNumber;

	// Use this for initialization
	void Start () {
		Destroy(gameObject, lifeTime);
	}
	
	public void SetAngularVelocity ()
	{
		rigidbody.angularVelocity = transform.up * slashSpeed;
	}
}
