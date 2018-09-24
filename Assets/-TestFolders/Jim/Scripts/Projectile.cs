using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    [HideInInspector] public float damage;
    [HideInInspector] public float range;
    [HideInInspector] public float speed;
    [HideInInspector] public bool explosive;
    [HideInInspector] public float explosionRadius;
    [HideInInspector] public GameObject onHitEffect;
    [HideInInspector] public GameObject trail;

    private Rigidbody rb;

    private void Start()
    {
        Collider[] colliders = GetComponents<Collider>();
        foreach (var item in colliders)
        {
            item.isTrigger = true;
        }
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.velocity = transform.forward * speed;
        if (trail != null) Instantiate(trail, transform);
        Invoke("OnRangeReached", range);
    }

    private void OnTriggerEnter(Collider other)
    {
        CancelInvoke();
        if (onHitEffect != null) Destroy(Instantiate(onHitEffect, transform.position, Quaternion.identity), 3f);
        if (explosive) Explode();
        else
        {
            Health target = other.GetComponentInParent<Health>();
            if (target != null)
            {
                target.TakeDamage(damage, transform.position);
            }
        }
        Destroy(gameObject);
    }

    private void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (var item in colliders)
        {
            Health target = item.GetComponentInParent<Health>();
            if (target != null)
            {
                target.TakeDamage(damage, transform.position);
            }
        }

    }

    private void OnRangeReached()
    {
        if (explosive) Explode();
        else Destroy(gameObject);
    }
}
