using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerInitializer : MonoBehaviour
{

	//TODO: Should be in Awake, need to check script order etc.
	private void Start()
	{
		//Initialize the GameManager
		GameManager.Instance.Initialize();
	}
}
