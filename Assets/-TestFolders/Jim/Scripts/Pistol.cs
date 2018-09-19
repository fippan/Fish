using UnityEngine;
using VRTK;

public class Pistol : Weapon
{
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
            controllerEvents = currentPrimaryGrabbingObject.GetComponent<VRTK_ControllerEvents>();

            if (VRTK_DeviceFinder.GetControllerHand(controllerEvents.gameObject) == SDK_BaseController.ControllerHand.Left)
            {
                interactableObject.allowedTouchControllers = VRTK_InteractableObject.AllowedController.LeftOnly;
                interactableObject.allowedUseControllers = VRTK_InteractableObject.AllowedController.LeftOnly;
            }
            else if (VRTK_DeviceFinder.GetControllerHand(controllerEvents.gameObject) == SDK_BaseController.ControllerHand.Right)
            {
                interactableObject.allowedTouchControllers = VRTK_InteractableObject.AllowedController.RightOnly;
                interactableObject.allowedUseControllers = VRTK_InteractableObject.AllowedController.RightOnly;
            }
        }
    }

    private void OnUngrab(object sender, InteractableObjectEventArgs e)
    {
        if (e.interactingObject == currentPrimaryGrabbingObject)
        {
            currentPrimaryGrabbingObject = null;
            currentSecondaryGrabbingObject = null;
            if (rb.isKinematic) rb.isKinematic = false;
            interactableObject.allowedTouchControllers = VRTK_InteractableObject.AllowedController.Both;
            interactableObject.allowedUseControllers = VRTK_InteractableObject.AllowedController.Both;
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

        if (hitScan)
            FireWithHitScan();
        else if (!hitScan)
            FireProjectile();

        OnShotFired();
        ReloadAndCooldown();
    }
}
