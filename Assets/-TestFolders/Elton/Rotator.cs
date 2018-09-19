using UnityEngine;

public class Rotator : MonoBehaviour
{
    public GameObject boat;
    public bool dead;
    [SerializeField] private float rotationSpeed;
    void Update()
    {
        if (boat != null)
            transform.position = boat.transform.position;
        Rotation();
    }

    public void Rotation()
    {
        if (!dead)
            transform.Rotate(0, -rotationSpeed, 0);

        if (dead && rotationSpeed >= 0)
        {
            rotationSpeed -= Time.deltaTime * 6;
            transform.Rotate(0, -rotationSpeed, 0);
        }
        else if (rotationSpeed < 0)
        {
            transform.Rotate(0, 0, 0);
        }
    }
}
