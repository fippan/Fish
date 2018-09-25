using UnityEngine;

public class ResetObjectPosition : MonoBehaviour
{
    private Transform[] resetPositions;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        Transform positions = GameObject.Find("ResetPositions").transform;
        resetPositions = new Transform[positions.childCount];
        for (int i = 0; i < positions.childCount; i++)
        {
            resetPositions[i] = positions.GetChild(i);
        }
    }

    private void Update()
    {
        if (transform.position.y < 0f)
        {
            ResetPositions();
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.transform.tag == "Water")
    //    {
    //        ResetPositions();
    //    }
    //}

    private void ResetPositions()
    {
        rb.velocity = Vector3.zero;
        int i = Random.Range(0, resetPositions.Length);
        transform.position = resetPositions[i].position;
    }
}
