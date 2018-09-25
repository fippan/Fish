using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerInitializer : MonoBehaviour
{
	private void Awake()
	{
		//Initialize the GameManager
		GameManager.Instance.Initialize();
	}
}
