using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canons : MonoBehaviour {

    [SerializeField] private GameObject canonlookat;
    [SerializeField] private float CanonNumber;
    private float timer;
    [SerializeField] private GameObject canonBall;


	void Start ()
    {
        //transform.rotation = Quaternion.LookRotation(transform.position - canonlookat.transform.position);
        transform.LookAt(canonlookat.transform);
        timer = Random.Range(10 ,14);
        InvokeRepeating("TakeThatShot", timer, timer);
    }

    private void TakeThatShot()
    {
        transform.LookAt(canonlookat.transform);
        var shootThatShit = GetComponent<Weapon>();
        shootThatShit.Shoot();
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
}
