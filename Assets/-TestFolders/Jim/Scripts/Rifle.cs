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

    protected override void Start()
    {
        base.Start();
        interactableObject = GetComponent<VRTK_InteractableObject>();
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

    //public void OnGrab()
    //{
    //    if (controllerEvents != null) return;

    //    controllerEvents = interactableObject.GetGrabbingObject().GetComponent<VRTK_ControllerEvents>();

    //    //Limit hands grabbing when picked up
    //    //if (VRTK_DeviceFinder.GetControllerHand(interactableObject.GetGrabbingObject()) == SDK_BaseController.ControllerHand.Left)
    //    //{
    //    //    interactableObject.allowedTouchControllers = VRTK_InteractableObject.AllowedController.LeftOnly;
    //    //    interactableObject.allowedUseControllers = VRTK_InteractableObject.AllowedController.LeftOnly;
    //    //}
    //    //else if (VRTK_DeviceFinder.GetControllerHand(interactableObject.GetGrabbingObject()) == SDK_BaseController.ControllerHand.Right)
    //    //{
    //    //    interactableObject.allowedTouchControllers = VRTK_InteractableObject.AllowedController.RightOnly;
    //    //    interactableObject.allowedUseControllers = VRTK_InteractableObject.AllowedController.RightOnly;
    //    //}
    //}

    //public void OnDrop()
    //{
    //    controllerEvents = null;
    //    //interactableObject.allowedTouchControllers = VRTK_InteractableObject.AllowedController.Both;
    //    //interactableObject.allowedUseControllers = VRTK_InteractableObject.AllowedController.Both;
    //}

    public override void Shoot()
    {
        if (!controllerEvents.IsButtonPressed(VRTK_ControllerEvents.ButtonAlias.TriggerClick))
            return;

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
            if (shotsFired > shotsUntilReload)
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
        if (hitScan)
            FireWithHitScan();
        else if (!hitScan)
            FireProjectile();

        OnShotFired();
    }
}
