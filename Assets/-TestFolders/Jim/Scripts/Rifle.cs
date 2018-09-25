using System.Collections;
using UnityEngine;
using VRTK;

public class Rifle : Weapon
{
    [Header("Rifle settings.")]
    public bool automatic;
    private bool isTriggerDown;
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
            if (spreadingBulletsEnabled) spreadingBullets = true;
            SetIKHand(e.interactingObject, true);
        }
        else
        {
            currentSecondaryGrabbingObject = e.interactingObject;
            spreadingBullets = false;
            SetIKHand(e.interactingObject, false);
        }
    }

    private void OnUngrab(object sender, InteractableObjectEventArgs e)
    {
        if (e.interactingObject == currentPrimaryGrabbingObject)
        {
            currentPrimaryGrabbingObject = null;
            currentSecondaryGrabbingObject = null;
            if (rb.isKinematic) rb.isKinematic = false;
            ResetIKHand();
        }
        else
        {
            currentSecondaryGrabbingObject = null;
            ResetIKHand(e.interactingObject);
        }
        spreadingBullets = spreadingBulletsEnabled;

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
            OnTriggerReleased();
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

        if (automatic && !isTriggerDown) StartCoroutine(AutomaticFire());
        else if (!automatic) SingleFire();
    }

    private IEnumerator AutomaticFire()
    {
        isTriggerDown = true;
        while (isTriggerDown)
        {
            Fire();
            if (shotsFired >= shotsUntilReload)
                yield return StartCoroutine(Reload());
            else
                yield return StartCoroutine(Cooldown());
        }
    }

    public void OnTriggerReleased()
    {
        isTriggerDown = false;
    }

    private void SingleFire()
    {
        Fire();
        ReloadAndCooldown();
    }

    private void Fire()
    {
        Haptics.Instance.StartHaptics(gameObject, hapticStrenght, hapticDuration, .01f);
        if (currentSecondaryGrabbingObject != null)
            Haptics.Instance.StartHaptics(gameObject, hapticStrenght, hapticDuration, .01f);
        if (hitScan)
            FireWithHitScan();
        else if (!hitScan)
            FireProjectile();

        OnShotFired();
    }
}
