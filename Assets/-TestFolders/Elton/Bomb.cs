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
        rb = GetComponent<Rigidbody>();
        startSize = transform.localScale;
        Throw(transform, GameObject.Find("Target").transform);
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
        Vector2 v2 = Random.insideUnitCircle;
        Vector3 v3 = new Vector3(v2.x, 0f, v2.y);
        Vector3 offset = v3.normalized * .8f;
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
            ICanTakeDamage target = hitColliders[i].GetComponentInParent<ICanTakeDamage>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }
            i++;
        }
        Destroy(Instantiate(explosionFX, transform.position, transform.rotation), 3f);
        //AudioManager.instance.Play("BombExplode");
        Destroy(gameObject);
    }
}
