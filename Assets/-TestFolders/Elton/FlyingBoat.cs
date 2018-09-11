using UnityEngine;

public class FlyingBoat : MonoBehaviour {
    [SerializeField] private float timeCounter;

    [SerializeField] private float speed;
    [SerializeField] private float width;
    [SerializeField] private float length;

    public GameObject boat;



	void Update () {
        timeCounter += Time.deltaTime * speed;

        float x = Mathf.Cos(timeCounter) * width;
        float y = 0;
        float z = Mathf.Sin(timeCounter) * length;

        transform.position = boat.transform.position + new Vector3(x, y, z);


        transform.Rotate(0,-0.05f, 0);
	}
}