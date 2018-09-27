using UnityEngine;

public class ShopCannon : MonoBehaviour
{
    [SerializeField] private Transform barrelEnd;
    [SerializeField] private Transform target;
    [SerializeField] private ParticleSystem ps;
    [SerializeField] private AudioSource audio;

    public void Shoot(GameObject gameObject)
    {
        gameObject.GetComponent<Rigidbody>().velocity = calcBallisticVelocityVector(barrelEnd.position, target.position, Random.Range(40f, 60f));
        gameObject.GetComponent<Rigidbody>().AddTorque(gameObject.transform.up * 10f);
        ps.Play();
        audio.Play();
    }

    private Vector3 calcBallisticVelocityVector(Vector3 source, Vector3 target, float angle)
    {
        Vector3 direction = target - source;
        float h = direction.y;
        direction.y = 0;
        float distance = direction.magnitude;
        float a = angle * Mathf.Deg2Rad;
        direction.y = distance * Mathf.Tan(a);
        distance += h / Mathf.Tan(a);

        // calculate velocity
        float velocity = Mathf.Sqrt(distance * Physics.gravity.magnitude / Mathf.Sin(2 * a));
        return velocity * direction.normalized;
    }
}
