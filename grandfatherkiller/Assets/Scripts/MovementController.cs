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
	public GameObject slash;
	public Transform shotSpawn;
	public float fireTime = 0.5f;
	public float meleeTime = 0.5f;
	public int maxHealth = 3;
	
	private float nextAttack;
	private int health;
	private bool dead;


	// Use this for initialization
	void Start () {
		health = maxHealth;
		dead = false;
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

			// Firing
			if (inputDevice.RightTrigger.IsPressed && Time.time > nextAttack)
			{	
				nextAttack = Time.time + fireTime;
				GameObject zBullet = (GameObject)Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
				zBullet.GetComponent<ShotController> ().SetVelocity ();
				zBullet.GetComponent<ShotController> ().playerNumber = playerNumber;
			}

			// Melee
			if (inputDevice.Action2.WasPressed && Time.time > nextAttack) {
				nextAttack = Time.time + meleeTime;
				GameObject zBullet = (GameObject)Instantiate (slash, shotSpawn.position, shotSpawn.rotation);
				zBullet.GetComponent<ShotController> ().SetVelocity ();
				zBullet.GetComponent<ShotController> ().playerNumber = playerNumber;
				zBullet.transform.parent = transform;
			}
		}
		controller.Move(moveDirection * Time.deltaTime);
		transform.forward = facingDirection;
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
			}
			Destroy(other.transform.parent.gameObject);
		} else if (other.tag == "Slash") {
			if (other.transform.parent.gameObject.GetComponent<ShotController>().playerNumber != playerNumber) {
				health -= 1;
				if (health <= 0) {
					dead = true;
				}
			}
		} 
	}
}
