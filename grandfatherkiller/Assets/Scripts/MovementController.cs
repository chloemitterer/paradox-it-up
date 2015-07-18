using UnityEngine;
using System.Collections;
using InControl;

[RequireComponent(typeof(CharacterController))]
public class MovementController : MonoBehaviour {
	public float maxSpeed = 5.0f;
	public int playerNumber = 1;

	private Vector3 moveDirection = Vector3.zero;
	private Vector3 facingDirection = Vector3.forward;

	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;
	
	private float nextFire;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		CharacterController controller = GetComponent<CharacterController>();

		var inputDevice = (InputManager.Devices.Count + 1 > playerNumber) ? InputManager.Devices[playerNumber - 1] : null;
		if (inputDevice != null) {
			moveDirection = Vector3.Normalize(new Vector3(inputDevice.Direction.X, 0, inputDevice.Direction.Y));
			if (inputDevice.RightStick.X != 0 || inputDevice.RightStick.Y != 0) {
				facingDirection = Vector3.Normalize(new Vector3(inputDevice.RightStick.X, 0, inputDevice.RightStick.Y));
			} else if (moveDirection != Vector3.zero) {
				facingDirection = moveDirection;
			}
			// moveDirection = transform.TransformDirection(moveDirection);
			moveDirection *= maxSpeed;

			//firing
			if (inputDevice.RightTrigger.IsPressed && Time.time > nextFire)
			{	
				nextFire = Time.time + 1.0f / fireRate;
				GameObject zBullet = (GameObject)Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
				zBullet.GetComponent<ShotController> ().SetVelocity ();
				
				
			}
		}
		controller.Move(moveDirection * Time.deltaTime);
		transform.forward = facingDirection;
	}
}
