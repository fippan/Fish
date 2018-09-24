using System.Collections;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private float timer;
    [SerializeField] private float damage;
    [SerializeField] private float radius;
    [SerializeField] private float firingAngle = 45f;
    [SerializeField] private float gravity = 9.8f;
    [SerializeField] private ParticleSystem explosionFX;

    private Rigidbody rb;
    private float timeActive = 0f;
    private Vector3 startSize;

    private void Start()
    {
    }

    private void Update()
    {
        Timer();
        AnimateBomb();
    }

    private void Timer()
    {
        if (timeActive < timer)
        {
            timeActive += Time.deltaTime;
            if (timeActive >= timer)
            {
                Explode();
            }
        }
    }

    private void AnimateBomb()
    {
        float percentage = timeActive / timer;
        transform.localScale = startSize * (1f + percentage);
    }

    public void Throw(Transform start, Transform target)
    {
        rb = GetComponent<Rigidbody>();
        startSize = transform.localScale;
        Vector2 v2 = Random.insideUnitCircle;
        Vector3 v3 = new Vector3(v2.x, 0f, v2.y);
        Vector3 offset = v3 * 1.3f;
        rb.velocity = calcBallisticVelocityVector(start.position, target.position + offset, firingAngle);
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

    private void Explode()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        int i = 0;
        while (i < hitColliders.Length)
        {
            Health target = hitColliders[i].GetComponentInParent<Health>();
            if (target != null)
            {
                target.TakeDamage(damage, hitColliders[i].transform.position);
            }
            i++;
        }
        ParticleSystem ps = Instantiate(explosionFX, transform.position, transform.rotation) as ParticleSystem;
        Destroy(ps.gameObject, 3f);
        Destroy(gameObject);
    }
}
