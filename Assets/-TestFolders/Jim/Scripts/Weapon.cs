﻿using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource), typeof(Animator))]
public abstract class Weapon : MonoBehaviour
{
    [Header("Weapon attributes.")]
    [Tooltip("Set true to enable hit scan weapon. False to shoot projectile.")]
    public bool hitScan;
    public bool spreadingBullets;
    [Tooltip("Deal damage over an area.")]
    public bool explosive;
    [Space]
    public float damage;
    [Tooltip("Time between shots in seconds.")]
    public float firingRate;
    public Vector2 minSpreadDegrees;
    public Vector2 maxSpreadDegrees;
    public float shotsUntilReload;
    [Tooltip("Time in seconds.")]
    public float reloadTime;
    [Space]
    public GameObject shellPrefab;
    public Transform shellPoint;
    public float shellForceMultiplier;
    [Tooltip("0 = do not destroy.")]
    public float shellLifeTime;
    [Space]
    [Tooltip("Weapon shoot sfx")]
    public AudioClip shootSFX;
    public AudioClip outOfAmmoSFX;

    [Header("Explosion settings.")]
    public float explosionRadius;

    [Header("Projectile settings.")]
    public Projectile projectile;
    [Tooltip("Time in seconds until destroyed automatically.")]
    public float lifetime;
    public float speed;
    public GameObject onShootEffect;
    public float onShootEffectLifetime;
    public GameObject onHitEffect;
    public float onHitEffectLifetime;
    public GameObject trail;

    [Header("Barrel")]
    public Transform barrelEnd;

    [Header("Haptic feedback.")]
    [Range(0, 1)]
    public float hapticStrenght;
    public float hapticDuration;

    private AudioSource audioSource;
    private Animator anim;
    protected bool canFire = true;
    protected bool isTriggerDown = false;
    protected float shotsFired;

    protected virtual void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = shootSFX;
        anim = GetComponent<Animator>();
    }

    public abstract void Shoot();

    protected void FireWithHitScan()
    {
        Vector3 direction = CalculateHitScanDirection();
        RaycastHit hit;

        if (Physics.Raycast(barrelEnd.position, direction, out hit, 100f))
            TargetHit(hit.transform, hit.point);
    }

    private Vector3 CalculateHitScanDirection()
    {
        if (!spreadingBullets)
            return barrelEnd.forward;

        Quaternion barrelEndStartRotation = barrelEnd.rotation;
        Vector3 newRotation = new Vector3(
            Random.Range(minSpreadDegrees.x, maxSpreadDegrees.x),
            Random.Range(minSpreadDegrees.y, maxSpreadDegrees.y),
            0f);
        barrelEnd.Rotate(newRotation);
        Vector3 direction = barrelEnd.forward;
        barrelEnd.rotation = barrelEndStartRotation;

        return direction;
    }

    private void TargetHit(Transform target, Vector3 point)
    {
        if (explosive)
        {
            Explode();
        }
        else
        {
            ICanTakeDamage targetHit = target.GetComponentInParent<ICanTakeDamage>();
            if (targetHit != null)
            {
                targetHit.TakeDamage(damage);
            }
        }

        if (onHitEffect != null)
        {
            Destroy(Instantiate(onHitEffect, point, Quaternion.identity), onHitEffectLifetime);
        }
    }

    private void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (var item in colliders)
        {
            ICanTakeDamage target = item.GetComponentInParent<ICanTakeDamage>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }
        }
    }

    protected void FireProjectile()
    {
        if (projectile == null)
        {
            Debug.LogWarning("Please assign a projectile in the inspector.");
            return;
        }

        if (spreadingBullets)
        {
            Projectile newProjectile = Instantiate(projectile, barrelEnd.position, CalculateProjectileRotation());
            SetProjectileValues(newProjectile);
        }
        else
        {
            Projectile newProjectile = Instantiate(projectile, barrelEnd.position, barrelEnd.rotation);
            SetProjectileValues(newProjectile);
        }
    }

    private Quaternion CalculateProjectileRotation()
    {
        Quaternion barrelEndStartRotation = barrelEnd.rotation;
        Vector3 newRotation = new Vector3(
            Random.Range(minSpreadDegrees.x, maxSpreadDegrees.x),
            Random.Range(minSpreadDegrees.y, maxSpreadDegrees.y),
            0f);
        barrelEnd.Rotate(newRotation);
        Quaternion rotation = barrelEnd.rotation;
        barrelEnd.rotation = barrelEndStartRotation;

        return rotation;
    }

    private void SetProjectileValues(Projectile newProjectile)
    {
        newProjectile.damage = damage;
        newProjectile.range = lifetime;
        newProjectile.speed = speed;
        newProjectile.explosive = explosive;
        newProjectile.explosionRadius = explosionRadius;
        newProjectile.onHitEffect = onHitEffect;
        newProjectile.trail = trail;
    }

    protected virtual void OnShotFired()
    {
        Haptics.Instance.StartHaptics(gameObject, hapticStrenght, hapticDuration, .01f);
        anim.SetTrigger("Single_Shot");
        audioSource.Play();

        if (onShootEffect != null)
            Destroy(Instantiate(onShootEffect, barrelEnd.position, barrelEnd.rotation), onShootEffectLifetime);

        if (shellPrefab != null)
        {
            GameObject newShell = Instantiate(shellPrefab, shellPoint.position, shellPoint.rotation);
            newShell.GetComponent<Rigidbody>().AddForce(shellPoint.forward * shellForceMultiplier);
            if (shellLifeTime > 0)
                Destroy(newShell, shellLifeTime);
        }

        shotsFired++;
    }

    protected void ReloadAndCooldown()
    {
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

    protected IEnumerator Cooldown()
    {
        canFire = false;
        yield return new WaitForSeconds(firingRate);
        canFire = true;
    }

    protected IEnumerator Reload()
    {
        anim.SetBool("Empty", true);
        yield return new WaitForSeconds(reloadTime);
        anim.SetBool("Empty", false);
        shotsFired = 0;
        canFire = true;
    }
}
