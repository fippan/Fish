using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Submarine : MonoBehaviour {


    private bool hasEnabled = false;
    public bool diveBack = false;

    public Transform playerPos;
    public Transform spawnpos;
    public GameObject explosionFX;
	// Use this for initialization
	void Start () {
        spawnpos = FindObjectOfType<DiverManager>().transform;
        Vector3 targetdir = transform.position - spawnpos.position;

        //Vector3 newDir = Vector3.RotateTowards(transform.right, targetdir, 100f, 0f);
        //transform.rotation = Quaternion.LookRotation(newDir);
        transform.LookAt(spawnpos);
	}
	
	// Update is called once per frame
	void Update () {
        if(transform.position.y < -1f)
        {
            if(!diveBack)
            {
                transform.position += new Vector3(0, 1 * Time.deltaTime, 0);
            }
        }
        else
        {
            if(!hasEnabled)
            {
                var tempEnemy = GetComponentInChildren<SubmarineEnemy>().canFire = true;
                hasEnabled = true;
            }
        }

        if(diveBack)
        {
            transform.position += new Vector3(0, 1 * -Time.deltaTime, 0);
            Destroy(gameObject, 4f);
        }
	}
}
