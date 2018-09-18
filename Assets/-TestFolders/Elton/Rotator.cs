using UnityEngine;

public class Rotator : MonoBehaviour {

    public GameObject boat;
    [SerializeField] private float rotationSpeed;
    void Update()
    {
        if(boat != null)
            transform.position = boat.transform.position;

        transform.Rotate(0, -rotationSpeed, 0);
    }
}
