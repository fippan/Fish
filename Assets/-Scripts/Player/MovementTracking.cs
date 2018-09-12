using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTracking : MonoBehaviour
{
	public Transform headProxy;

	private void Update()
	{
		//Set rotation and position for head
		transform.GetChild(0).transform.rotation = headProxy.transform.rotation;
		transform.GetChild(0).transform.position = headProxy.transform.position;

		//Set rotation and position for body (Looking towards head all the time)
		transform.GetChild(1).transform.LookAt(transform.GetChild(0));
		transform.GetChild(1).transform.position = headProxy.transform.position;


		//Set position for body and head
		//transform.position = headProxy.transform.position;
	}
}
