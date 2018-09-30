using UnityEngine;
using System.Collections;

public class ShopCannon : MonoBehaviour
{
    [SerializeField] private Transform barrelEnd;
    [SerializeField] private Transform target;
    [SerializeField] private ParticleSystem ps;
    [SerializeField] private AudioSource audio;

    public void Shoot(GameObject gameObject)
    {
        gameObject.GetComponent<Rigidbody>().velocity = calcBallisticVelocityVector(barrelEnd.position, target.position, 40/*Random.Range(40f, 60f)*/);
        gameObject.GetComponent<Rigidbody>().AddTorque(gameObject.transform.up * 10f);
        ps.Play();
        audio.Play();
        SlowMotion.Instance.StartSlowMotion(2, 5.3f);
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

    private IEnumerator SlowMo ()
    {
        float myTime = 0;
        while (myTime < 5.3f)
        {
            myTime += Time.unscaledDeltaTime;
            yield return null;
        }
        Time.timeScale = 0.05f;
        myTime = 0;
        while (myTime < 2f)
        {
            myTime += Time.unscaledDeltaTime;
            yield return null;
        }

        Time.timeScale = 1;
    }
}
