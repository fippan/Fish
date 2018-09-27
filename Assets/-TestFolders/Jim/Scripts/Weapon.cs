using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(BulletTrace))]
public abstract class Weapon : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject magPrefab;
    public GameObject casingPrefab;
    public GameObject onShootEffect;
    public GameObject trail;
    public Projectile projectile;

    [Header("Transforms")]
    public Transform hitScanPoint;
    public Transform magPoint;
    public Transform casingPoint;
    public Transform barrelEnd;
    protected Transform leftController;
    protected Transform rightController;

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
    public float magForceMultiplier;
    public float casingForceMultiplier;
    [Tooltip("0 = do not destroy.")]
    public float magLifeTime;
    [Tooltip("0 = do not destroy.")]
    public float casingLifeTime;

    [Header("Explosion settings.")]
    public float explosionRadius;

    [Header("Projectile settings.")]
    [Tooltip("Time in seconds until destroyed automatically.")]
    public float lifetime;
    public float speed;
    public float onShootEffectLifetime;
    [Tooltip("0 = Humanoid, 1 = Water, 2 = Metal, 3 = Wood.")]
    public float onHitEffectLifetime;

    [Header("Haptic feedback.")]
    [Range(0, 1)]
    public float hapticStrenght;
    public float hapticDuration;

    protected WeaponAudioManager weaponAudioManager;
    protected BulletTrace bulletTrace;
    protected AudioSource audioSource;
    protected Animator anim;
    protected GameObject currentMag;
    protected bool canFire = true;
    protected bool spreadingBulletsEnabled;
    protected bool reloading;
    protected float shotsFired;

    protected virtual void Start()
    {
        bulletTrace = GetComponent<BulletTrace>();
        weaponAudioManager = GetComponent<WeaponAudioManager>();
        anim = GetComponent<Animator>();
        if (magPrefab != null) currentMag = Instantiate(magPrefab, magPoint);
        spreadingBulletsEnabled = spreadingBullets;
    }

    public abstract void Shoot();

    protected void FireWithHitScan()
    {
        Vector3 direction = CalculateHitScanDirection();
        RaycastHit hit;

        if (Physics.Raycast(hitScanPoint.position, direction, out hit, 300f))
        {
            TargetHit(hit.transform, hit.point);
            bulletTrace.NewTrace(barrelEnd.position, hit.point);
        }
        else
        {
            bulletTrace.NewTrace(barrelEnd.position, barrelEnd.forward * 50f);
        }
    }

    private Vector3 CalculateHitScanDirection()
    {
        if (!spreadingBullets)
            return hitScanPoint.forward;

        Quaternion hitScanPointStartRotation = hitScanPoint.rotation;
        Vector3 newRotation = new Vector3(
            Random.Range(minSpreadDegrees.x, maxSpreadDegrees.x),
            Random.Range(minSpreadDegrees.y, maxSpreadDegrees.y),
            0f);
        hitScanPoint.Rotate(newRotation);
        Vector3 direction = hitScanPoint.forward;
        hitScanPoint.rotation = hitScanPointStartRotation;

        return direction;
    }

    private void TargetHit(Transform target, Vector3 point)
    {
        if (explosive)
        {
            Explode(point);
        }
        else
        {
            Health targetHealth = target.GetComponentInParent<Health>();
            if (targetHealth != null)
            {
                targetHealth.TakeDamage(damage, point);
            }
        }
    }

    private void Explode(Vector3 point)
    {
        Collider[] colliders = Physics.OverlapSphere(point, explosionRadius);
        foreach (var item in colliders)
        {
            Health targetHealth = item.GetComponentInParent<Health>();
            if (targetHealth != null)
            {
                targetHealth.TakeDamage(damage, point);
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
        newProjectile.trail = trail;
    }

    protected virtual void OnShotFired()
    {
        weaponAudioManager.Play("Fire");
        if (anim.runtimeAnimatorController != null)
            anim.SetTrigger("Single_Shot");
        if (onShootEffect != null)
            Destroy(Instantiate(onShootEffect, barrelEnd.position, barrelEnd.rotation), onShootEffectLifetime);

        if (casingPrefab != null)
        {
            GameObject newShell = Instantiate(casingPrefab, casingPoint.position, casingPoint.rotation);
            newShell.GetComponent<Rigidbody>().AddForce(casingPoint.forward * Random.Range(casingForceMultiplier * .8f, casingForceMultiplier * 1.2f));
            if (casingLifeTime > 0)
                Destroy(newShell, casingLifeTime);
            weaponAudioManager.Play("Casing");
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
        reloading = true;
        float magDelay = .2f;
        float reload = reloadTime - magDelay;
        if (reload < .2f)
        {
            reload = reloadTime;
            magDelay = 0f;
        }

        canFire = false;
        weaponAudioManager.Play("Reload");
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
            Destroy(currentMag, magLifeTime);
        }

        yield return new WaitForSeconds(reload);

        weaponAudioManager.Play("Reload");
        if (anim.runtimeAnimatorController != null)
            anim.SetBool("Empty", false);
        if (magPrefab != null)
        {
            currentMag = Instantiate(magPrefab, magPoint);
        }
        reloading = false;
        shotsFired = 0;
        canFire = true;
    }
}
