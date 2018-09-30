using UnityEngine;

[RequireComponent(typeof(AudioController))]
public class Health : MonoBehaviour
{
    [SerializeField]
    protected float maxHealth;
    [SerializeField]
    protected float currentHealth;
    [SerializeField]
    protected bool indestructible;
    [SerializeField]
    protected GameObject onHitEffect;
    [SerializeField]
    protected float onHitEffectLifetime;

    protected AudioController audioController;
    protected bool dead;

    protected virtual void Start()
    {
        currentHealth = maxHealth;
        audioController = GetComponent<AudioController>();
    }

    public virtual void TakeDamage(float amount, Vector3 point)
    {
        if (!indestructible)
            currentHealth -= amount;
        audioController.Play("Hit", point);
        if (onHitEffect != null)
            Destroy(Instantiate(onHitEffect, point, Quaternion.identity), onHitEffectLifetime);

        if (currentHealth <= 0)
        {
            if (!indestructible)
                Death();
        }
    }

    protected virtual void Death()
    {
        audioController.Play("Death", transform.position);
        dead = true;
        KillCountManager.Instance.AddKill();
        Destroy(gameObject);
    }
}
