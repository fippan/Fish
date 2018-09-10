using UnityEngine;

public class FlyingBoat : MonoBehaviour {
    [SerializeField] private float timeCounter;
    public GameObject boat;

	void Update () {
        timeCounter += Time.deltaTime;

        transform.position = boat.transform.position;

        transform.Rotate(0,-0.05f, 0);
	}
}