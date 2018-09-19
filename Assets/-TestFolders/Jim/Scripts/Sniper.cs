using System.Collections;
using UnityEngine;
using VRTK;

public class Sniper : Weapon
{

    [Header("Sniper settings.")]
    public GameObject scopeCamera;

    private GameObject currentPrimaryGrabbingObject;
    private GameObject currentSecondaryGrabbingObject;
    private VRTK_InteractableObject interactableObject;
    private VRTK_ControllerEvents controllerEvents;

    private Rigidbody rb;

    private void Awake()
    {
        interactableObject = GetComponent<VRTK_InteractableObject>();
    }

    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        interactableObject.InteractableObjectGrabbed += OnGrab;
        interactableObject.InteractableObjectUngrabbed += OnUngrab;
        interactableObject.InteractableObjectUnused += OnUnuse;
        interactableObject.InteractableObjectUsed += OnUse;
    }

    private void OnGrab(object sender, InteractableObjectEventArgs e)
    {
        if (currentPrimaryGrabbingObject == null)
        {
            currentPrimaryGrabbingObject = e.interactingObject;
            scopeCamera.SetActive(true);
        }
        else
        {
            currentSecondaryGrabbingObject = e.interactingObject;
        }
    }

    private void OnUngrab(object sender, InteractableObjectEventArgs e)
    {
        if (e.interactingObject == currentPrimaryGrabbingObject)
        {
            currentPrimaryGrabbingObject = null;
            currentSecondaryGrabbingObject = null;
            if (rb.isKinematic) rb.isKinematic = false;
            scopeCamera.SetActive(false);
        }
        else
        {
            currentSecondaryGrabbingObject = null;
        }
    }

    private void OnUse(object sender, InteractableObjectEventArgs e)
    {
        if (e.interactingObject == currentPrimaryGrabbingObject)
        {
            Shoot();
        }
    }

    private void OnUnuse(object sender, InteractableObjectEventArgs e)
    {
        if (e.interactingObject == currentPrimaryGrabbingObject)
        {

        }
    }

    private void OnDisable()
    {
        interactableObject.InteractableObjectGrabbed -= OnGrab;
        interactableObject.InteractableObjectUngrabbed -= OnUngrab;
        interactableObject.InteractableObjectUnused -= OnUnuse;
        interactableObject.InteractableObjectUsed -= OnUse;
    }

    public override void Shoot()
    {
        if (!canFire) return;

        Haptics.Instance.StartHaptics(gameObject, hapticStrenght, hapticDuration, .01f);
        if (currentSecondaryGrabbingObject != null)
            Haptics.Instance.StartHaptics(gameObject, hapticStrenght, hapticDuration, .01f);
        canFire = false;
        if (hitScan)
            FireWithHitScan();
        else if (!hitScan)
            FireProjectile();

        OnShotFired();
        ReloadAndCooldown();
    }
    protected override void OnShotFired()
    {
        if (shootSFX != null)
            audioSource.Play();
        if (onShootEffect != null)
            Destroy(Instantiate(onShootEffect, barrelEnd.position, barrelEnd.rotation), onShootEffectLifetime);
        shotsFired++;
    }

    protected override void ReloadAndCooldown()
    {
        if (shotsFired >= shotsUntilReload)
        {
            StartCoroutine(ReloadSnper());
            return;
        }
        else
        {
            StartCoroutine(CooldownSniper());
        }
    }

    protected IEnumerator CooldownSniper()
    {
        float shellDelay = .2f;
        float reload = firingRate - shellDelay;
        if (reload < .2f)
        {
            reload = reloadTime;
            shellDelay = 0f;
        }

        if (anim.runtimeAnimatorController != null)
            anim.SetTrigger("Reload");

        yield return new WaitForSeconds(shellDelay);

        if (shellPrefab != null)
        {
            GameObject newShell = Instantiate(shellPrefab, shellPoint.position, shellPoint.rotation);
            newShell.GetComponent<Rigidbody>().AddForce(shellPoint.forward * Random.Range(shellForceMultiplier * .8f, shellForceMultiplier * 1.2f));
            if (shellLifeTime > 0)
                Destroy(newShell, shellLifeTime);
        }

        yield return new WaitForSeconds(reload);

        canFire = true;
    }

    private IEnumerator ReloadSnper()
    {
        float magDelay = .2f;
        float reloadDelay = .4f;
        float reload = reloadTime - magDelay - reloadDelay;
        if (reload < magDelay + reloadDelay)
        {
            reload = reloadTime;
            magDelay = 0f;
            reloadDelay = 0f;
        }

        if (anim.runtimeAnimatorController != null)
            anim.SetTrigger("Reload");

        yield return new WaitForSeconds(magDelay);

        if (shellPrefab != null)
        {
            GameObject newShell = Instantiate(shellPrefab, shellPoint.position, shellPoint.rotation);
            newShell.GetComponent<Rigidbody>().AddForce(shellPoint.forward * Random.Range(shellForceMultiplier * .8f, shellForceMultiplier * 1.2f));
            if (shellLifeTime > 0)
                Destroy(newShell, shellLifeTime);
        }

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

        if (magPrefab != null)
            currentMag = Instantiate(magPrefab, magPoint);
        if (anim.runtimeAnimatorController != null)
            anim.SetTrigger("Reload");

        yield return new WaitForSeconds(reloadDelay);
        shotsFired = 0;
        canFire = true;
    }
}
