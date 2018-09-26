using UnityEngine;

public class MovementTracking : MonoBehaviour
{
	public Transform headProxy;

    private Transform head;
    private Transform body;

    private void Start()
    {
        head = transform.GetChild(0);
        body = transform.GetChild(1);
    }

    private void Update()
	{
		//Set rotation and position for head
		head.rotation = headProxy.transform.rotation;
		head.position = headProxy.transform.position;

		//Set rotation, position and scale for body (Looking towards head all the time)
		body.LookAt(transform.GetChild(0));
        float distance = Vector3.Distance(body.position, head.position);
        Vector3 newScaleY = new Vector3(body.localScale.x, body.localScale.y, distance - head.localScale.z);
        body.localScale = newScaleY;
	}
}
