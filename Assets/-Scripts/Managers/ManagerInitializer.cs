using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerInitializer : MonoBehaviour
{
	//public static 

	//TODO: Should be in Awake, need to check script order etc.
	private void Start()
	{
		//Instantiate game states
		

		//Initialize the GameManager
		GameManager.Instance.Initialize();
	}
}
