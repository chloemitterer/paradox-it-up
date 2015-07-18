using UnityEngine;
using System.Collections;
using InControl;

[RequireComponent(typeof(CharacterController))]
public class MovementController : MonoBehaviour {
	public float maxSpeed = 5.0f;
	public int playerNumber = 1;

	private Vector3 moveDirection = Vector3.zero;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		CharacterController controller = GetComponent<CharacterController>();

		var inputDevice = (InputManager.Devices.Count + 1 > playerNumber) ? InputManager.Devices[playerNumber - 1] : null;
		if (inputDevice != null) {
			moveDirection = new Vector3(inputDevice.Direction.X, 0, inputDevice.Direction.Y);
			moveDirection = transform.TransformDirection(moveDirection);
			moveDirection *= maxSpeed;
		}
		controller.Move(moveDirection * Time.deltaTime);
	}
}
