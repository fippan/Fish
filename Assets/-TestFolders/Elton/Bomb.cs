using System.Collections;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private float timer;
    [SerializeField] private float damage;
    [SerializeField] private float radius;
    [SerializeField] private float firingAngle = 45f;
    [SerializeField] private float gravity = 9.8f;

    private Rigidbody rb;
    private float timeActive = 0f;
    private Vector3 startSize;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
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
        //Vector3 targetPos = target.transform.position = Random.insideUnitCircle * 1;
        StartCoroutine(SimulateProjectile(start, target));
    }

    IEnumerator SimulateProjectile(Transform start, Transform target)
    {
        // Short delay added before Projectile is thrown
        //yield return new WaitForSeconds(1.5f);

        // Move projectile to the position of throwing object + add some offset if needed.
        Vector2 offset = Random.insideUnitCircle;
        transform.position = start.position + new Vector3(offset.x + .5f, 0.0f, offset.y + .5f);

        // Calculate distance to target
        float target_Distance = Vector3.Distance(transform.position, target.position);

        // Calculate the velocity needed to throw the object to the target at specified angle.
        float projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);

        // Extract the X  Y componenent of the velocity
        float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
        float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

        // Calculate flight time.
        float flightDuration = target_Distance / Vx;

        // Rotate projectile to face the target.
        transform.rotation = Quaternion.LookRotation(target.position - transform.position);

        float elapse_time = 0;

        while (elapse_time < flightDuration)
        {
            transform.Translate(0, (Vy - (gravity * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);

            elapse_time += Time.deltaTime;

            yield return null;
        }
        rb.isKinematic = false;
        rb.useGravity = true;
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
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
