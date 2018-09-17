using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource), typeof(Animator))]
public class Weapon : MonoBehaviour
{
    [Header("Weapon attributes.")]
    [Tooltip("Set true to enable hit scan weapon. False to shoot projectile.")]
    public bool hitScan;
    [Tooltip("Automatic fire when trigger is held down.")]
    public bool automatic;
    public bool spreadingBullets;
    public bool burst;
    public int bulletsToBurst;
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
    
    private AudioSource audioSource;
    private Animator anim;
    private bool canFire = true;
    private bool isTriggerDown = false;
    private float shotsFired;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = shootSFX;

        anim = GetComponent<Animator>();
    }

    public void Shoot()
    {
        if (automatic)
        {
            StartCoroutine(AutomaticFire());
            return;
        }

        if (!canFire) return;

        if (canFire)
        {
            if (burst)
                Burst();
            else
                Fire();

            anim.SetTrigger("Single_Shot");
            audioSource.Play();

            if (onShootEffect != null)
                Destroy(Instantiate(onShootEffect, barrelEnd.position, barrelEnd.rotation), onShootEffectLifetime);

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

    private void Burst()
    {
        for (int i = 0; i < bulletsToBurst; i++)
        {
            Fire();
        }
    }

    public void OnTriggerReleased()
    {
        isTriggerDown = false;
    }

    private IEnumerator AutomaticFire()
    {
        isTriggerDown = true;
        while (isTriggerDown)
        {
            Fire();
            anim.SetTrigger("Single_Shot");
            audioSource.Play();
            shotsFired++;

            if (onShootEffect != null)
                Destroy(Instantiate(onShootEffect, barrelEnd.position, barrelEnd.rotation), onShootEffectLifetime);
            
            if (shotsFired > shotsUntilReload)
                yield return StartCoroutine(Reload());
            else
                yield return new WaitForSeconds(firingRate);
        }
    }

    private void Fire()
    {
        if (hitScan) FireWithHitScan();
        else FireProjectile();
        if (shellPrefab != null)
        {
            GameObject newShell = Instantiate(shellPrefab, shellPoint.position, shellPoint.rotation);
            newShell.GetComponent<Rigidbody>().AddForce(Vector3.forward * shellForceMultiplier);
            if (shellLifeTime > 0)
                Destroy(newShell, shellLifeTime);
        }
    }

    private void FireWithHitScan()
    {
        if (spreadingBullets)
        {
            Vector3 direction = CalculateHitScanDirection();
            RaycastHit hit;
            if (Physics.Raycast(barrelEnd.position, direction, out hit, 100f))
                TargetHit(hit.transform, hit.point);
        }
        else
        {
            RaycastHit hit;
            if (Physics.Raycast(barrelEnd.position, barrelEnd.forward, out hit, 100f))
                TargetHit(hit.transform, hit.point);
        }
    }

    private Vector3 CalculateHitScanDirection()
    {
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
        ICanTakeDamage targetHit = target.GetComponentInParent<ICanTakeDamage>();
        if (targetHit != null)
        {
            targetHit.TakeDamage(damage);
        }

        if (onHitEffect != null)
        {
            Destroy(Instantiate(onHitEffect, point, Quaternion.identity), onHitEffectLifetime);
        }
    }

    private void FireProjectile()
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

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(firingRate);

        canFire = true;
    }

    private IEnumerator Reload()
    {
        anim.SetBool("Empty", true);
        yield return new WaitForSeconds(reloadTime);
        anim.SetBool("Empty", false);
        shotsFired = 0;
        canFire = true;
    }
}
