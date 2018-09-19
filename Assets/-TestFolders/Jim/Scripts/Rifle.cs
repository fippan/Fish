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
        if (!canFire) return;

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
        Haptics.Instance.StartHaptics(currentPrimaryGrabbingObject, hapticStrenght, hapticDuration, 0f);
        if (currentSecondaryGrabbingObject != null)
            Haptics.Instance.StartHaptics(currentSecondaryGrabbingObject, hapticStrenght, hapticDuration, 0f);
        if (hitScan)
            FireWithHitScan();
        else if (!hitScan)
            FireProjectile();

        OnShotFired();
    }
}
