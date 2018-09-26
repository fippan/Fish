using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerInitializer : MonoBehaviour
{
	private void Awake()
	{
		GameManager.Instance.Initialize();
	}
}
