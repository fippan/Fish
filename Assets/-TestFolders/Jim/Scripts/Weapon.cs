using System.Collections;
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
    public Transform magPoint;
    public GameObject magPrefab;
    public float magForceMultiplier;
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
    [Tooltip("0 = Humanoid, 1 = Water, 2 = Metal, 3 = Wood.")]
    public GameObject[] onHitEffects = new GameObject[3];
    public float onHitEffectLifetime;
    public GameObject trail;

    [Header("Barrel")]
    public Transform barrelEnd;

    [Header("Haptic feedback.")]
    [Range(0, 1)]
    public float hapticStrenght;
    public float hapticDuration;

    protected AudioSource audioSource;
    protected Animator anim;
    protected bool canFire = true;
    protected float shotsFired;
    protected GameObject currentMag;
    protected bool spreadingBulletsEnabled;

    protected virtual void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (shootSFX != null) audioSource.clip = shootSFX;
        anim = GetComponent<Animator>();
        if (magPrefab != null) currentMag = Instantiate(magPrefab, magPoint);
        spreadingBulletsEnabled = spreadingBullets;
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

        OnHitTargetEffect(target, point);
    }

    private void OnHitTargetEffect(Transform target, Vector3 point)
    {
        int i;
        switch (target.tag)
        {
            case "Humanoid":
                i = 0;
                break;
            case "Water":
                i = 1;
                break;
            case "Metal":
                i = 2;
                break;
            case "Wood":
                i = 3;
                break;
            default:
                i = 2;
                break;
        }

        if (onHitEffects[i] != null)
            Destroy(Instantiate(onHitEffects[i], point, Quaternion.identity), onHitEffectLifetime);
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
        newProjectile.onHitEffect = onHitEffects[0];
        newProjectile.trail = trail;
    }

    protected virtual void OnShotFired()
    {
        //Haptics.Instance.StartHaptics(gameObject, hapticStrenght, hapticDuration, .01f);
        if (anim.runtimeAnimatorController != null)
            anim.SetTrigger("Single_Shot");
        if (shootSFX != null)
            audioSource.Play();
        if (onShootEffect != null)
            Destroy(Instantiate(onShootEffect, barrelEnd.position, barrelEnd.rotation), onShootEffectLifetime);

        if (shellPrefab != null)
        {
            GameObject newShell = Instantiate(shellPrefab, shellPoint.position, shellPoint.rotation);
            newShell.GetComponent<Rigidbody>().AddForce(shellPoint.forward * Random.Range(shellForceMultiplier * .8f, shellForceMultiplier * 1.2f));
            if (shellLifeTime > 0)
                Destroy(newShell, shellLifeTime);
        }

        shotsFired++;
    }

    protected virtual void ReloadAndCooldown()
    {
        if (shotsFired >= shotsUntilReload)
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
        float magDelay = .2f;
        float reload = reloadTime - magDelay;
        if (reload < .2f)
        {
            reload = reloadTime;
            magDelay = 0f;
        }

        canFire = false;

        if (anim.runtimeAnimatorController != null)
            anim.SetBool("Empty", true);

        yield return new WaitForSeconds(magDelay);

        if (magPrefab != null)
        {
            currentMag.transform.parent = null;
            currentMag.GetComponent<BoxCollider>().isTrigger = false;
            Rigidbody magRb = currentMag.GetComponent<Rigidbody>();
            magRb.isKinematic = false;
            magRb.AddForce((magPoint.up * -1) * magForceMultiplier);
            Destroy(currentMag, 5f);
        }

        yield return new WaitForSeconds(reload);

        if (anim.runtimeAnimatorController != null)
            anim.SetBool("Empty", false);
        if (magPrefab != null)
        {
            currentMag = Instantiate(magPrefab, magPoint);
        }
        shotsFired = 0;
        canFire = true;
    }
}
