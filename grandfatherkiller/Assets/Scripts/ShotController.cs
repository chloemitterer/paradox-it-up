using UnityEngine;
using System.Collections;

public class ShotController : MonoBehaviour {

	public int shotSpeed;

	// Use this for initialization
	void Start () {
	
	}
	
	public void SetVelocity ()
	{
		rigidbody.velocity = transform.forward * shotSpeed;
	}
}
