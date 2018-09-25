using System.Collections;
using UnityEngine;
using VRTK;

public class Shotgun : Weapon
{
    [Header("Shotgun settings.")]
    public int numberOfBulletsToBurst;

    private GameObject currentPrimaryGrabbingObject;
    private GameObject currentSecondaryGrabbingObject;
    private VRTK_InteractableObject interactableObject;
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
        if (!canFire)
        {
            if (reloading)
                weaponAudioManager.PlayOneShot("DryFire");
            return;
        }

        Haptics.Instance.StartHaptics(gameObject, hapticStrenght, hapticDuration, .01f);
        if (currentSecondaryGrabbingObject != null)
            Haptics.Instance.StartHaptics(gameObject, hapticStrenght, hapticDuration, .01f);
        for (int i = 0; i < numberOfBulletsToBurst; i++)
            if (hitScan)
                FireWithHitScan();
            else if (!hitScan)
                FireProjectile();

        OnShotFired();
        StartCoroutine(ReloadShotgun());
    }

    protected override void OnShotFired()
    {
        weaponAudioManager.PlayOneShot("Fire");
        if (onShootEffect != null)
            Destroy(Instantiate(onShootEffect, barrelEnd.position, barrelEnd.rotation), onShootEffectLifetime);
        shotsFired++;
    }

    private IEnumerator ReloadShotgun()
    {
        canFire = false;
        float shellDelay = .2f;
        float reload = reloadTime - shellDelay;
        if (reload < .2f)
        {
            reload = reloadTime;
            shellDelay = 0f;
        }

        if (anim.runtimeAnimatorController != null)
            anim.SetTrigger("Reload");
        weaponAudioManager.PlayOneShot("Reload");

        yield return new WaitForSeconds(shellDelay);
        
        if (casingPrefab != null)
        {
            GameObject newShell = Instantiate(casingPrefab, casingPoint.position, casingPoint.rotation);
            newShell.GetComponent<Rigidbody>().AddForce(casingPoint.forward * Random.Range(casingForceMultiplier * .8f, casingForceMultiplier * 1.2f));
            if (casingLifeTime > 0)
                Destroy(newShell, casingLifeTime);
            weaponAudioManager.PlayOneShot("Casing");
        }

        yield return new WaitForSeconds(reload);

        shotsFired = 0;
        canFire = true;
    }

}
