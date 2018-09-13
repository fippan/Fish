using System.Collections;
using UnityEngine;
using VRTK;

public class Weapon : MonoBehaviour
{
    [Header("Weapon attributes.")]
    public float damage;
    [Tooltip("Minimum time between shots in seconds.")]
    public float firingRate;
    public float shotsUntilReload;
    [Tooltip("Time in seconds.")]
    public float reloadTime;
    [Tooltip("Time in seconds until destroyed automatically.")]
    public float range;
    public float speed;

    [Header("Explosion settings.")]
    public bool explosive;
    public float explosionRadius;

    [Header("Projectile settings.")]
    public Projectile projectile;
    public GameObject onShootEffect;
    public float onShootEffectLifetime;
    public GameObject onHitEffect;
    public float onHitEffectLifetime;
    public GameObject trail;

    [Header("Haptic feedback.")]
    [Range(0, 1)]
    public float hapticStrenght;

    [Header("Barrel")]
    public Transform barrelEnd;

    private VRTK_ControllerHaptics controllerHaptics;
    private bool canFire = true;
    private float shotsFired;

    private void Start()
    {
        controllerHaptics = GetComponent<VRTK_ControllerHaptics>();
    }

    public void Shoot()
    {
        if (!canFire) return;

        if (canFire)
        {
            Fire();
            if (onShootEffect != null) Destroy(Instantiate(onShootEffect, barrelEnd.position, barrelEnd.rotation), onShootEffectLifetime);            
            shotsFired++;
            canFire = false;
            if (shotsFired > shotsUntilReload)
            {
                StartCoroutine(Reload());
                return;
            }
            else
            {
                StartCoroutine(Cooldown());
            }
        }
    }

    public void HapticFeedback()
    {
        if (canFire)
            VRTK_ControllerHaptics.TriggerHapticPulse
                (VRTK_ControllerReference.GetControllerReference(transform.parent.gameObject), hapticStrenght);
    }

    private void Fire()
    {
        Projectile newProjectile = Instantiate(projectile, barrelEnd.position, barrelEnd.rotation);
        newProjectile.damage = damage;
        newProjectile.range = range;
        newProjectile.speed = speed;
        newProjectile.explosive = explosive;
        newProjectile.explosionRadius = explosionRadius;
        newProjectile.onHitEffect = onHitEffect;
        newProjectile.trail = trail;
    }

    private IEnumerator Cooldown()
    {
        float timeSinceLastShot = 0;

        while (timeSinceLastShot < firingRate)
        {
            timeSinceLastShot += Time.deltaTime;
            yield return null;
        }

        canFire = true;
    }

    private IEnumerator Reload()
    {
        float timeLeft = reloadTime;

        while (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            yield return null;
        }

        shotsFired = 0;
        canFire = true;
    }
}
