using UnityEngine;
using System.Collections;

public class ShotController : MonoBehaviour {

	public float shotSpeed;
	public float lifeTime;
	public int playerNumber;

	// Use this for initialization
	void Start () {
		Destroy(gameObject, lifeTime);
	}
	
	public void SetVelocity ()
	{
		rigidbody.velocity = transform.forward * shotSpeed;
	}
}
