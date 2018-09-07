using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayAndNight : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.RotateAround(Vector3.zero, Vector3.right, 0.05f * Time.deltaTime);
        transform.LookAt(Vector3.zero);
	}
}
