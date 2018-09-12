using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTracking : MonoBehaviour
{
	public Transform headProxy;

	private void Update()
	{
		//Set rotation for head
		transform.GetChild(0).transform.rotation = headProxy.transform.rotation;
		transform.GetChild(1).transform.LookAt(transform.GetChild(0));

		//Set position for body and head
		transform.position = headProxy.transform.position;


		//transform.rotation = headProxy.transform.rotation;
	}
}
