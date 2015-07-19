using UnityEngine;
using System.Collections;
using InControl;

public class StageSelect : MonoBehaviour {
	void Update() {
		var inputDevice = InputManager.ActiveDevice;
		if (inputDevice.Action1.WasPressed) {
			Application.LoadLevel ("Modern");
		} else if (inputDevice.Action2.WasPressed) {
			Application.LoadLevel ("Medieval");
		} else if (inputDevice.MenuWasPressed) {
			Application.Quit();
		}
	}	
}