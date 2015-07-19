using UnityEngine;
using System.Collections;

public class RestartGame : MonoBehaviour 
{
	
	public void BeginGame () 
	{
		Application.LoadLevel ("startscreen");
	}

	public void MedievalGame () 
	{
		Application.LoadLevel ("Medieval");
	}

	public void ModernGame () 
	{
		Application.LoadLevel ("Modern");
	}
}