using System.Collections;
using UnityEngine;
using VRTK;

public class Rifle : Weapon
{
    [Header("Rifle settings.")]
    public bool automatic;
    private bool isTriggerDown;
    private GameObject controllerObject;
    private VRTK_InteractableObject interactableObject;
    private VRTK_ControllerEvents controllerEvents;
    private string controllerHand;

    protected override void Start()
    {
        base.Start();
        interactableObject = GetComponent<VRTK_InteractableObject>();
    }

    public void OnGrab()
    {
        //controllerEvents = interactableObject.GetGrabbingObject().GetComponent<VRTK_ControllerEvents>();

        //Limit hands grabbing when picked up
        if (VRTK_DeviceFinder.GetControllerHand(interactableObject.GetGrabbingObject()) == SDK_BaseController.ControllerHand.Left)
        {
            interactableObject.allowedTouchControllers = VRTK_InteractableObject.AllowedController.LeftOnly;
            interactableObject.allowedUseControllers = VRTK_InteractableObject.AllowedController.LeftOnly;
        }
        else if (VRTK_DeviceFinder.GetControllerHand(interactableObject.GetGrabbingObject()) == SDK_BaseController.ControllerHand.Right)
        {
            interactableObject.allowedTouchControllers = VRTK_InteractableObject.AllowedController.RightOnly;
            interactableObject.allowedUseControllers = VRTK_InteractableObject.AllowedController.RightOnly;
        }
    }

    public void OnDrop()
    {
        interactableObject.allowedTouchControllers = VRTK_InteractableObject.AllowedController.Both;
        interactableObject.allowedUseControllers = VRTK_InteractableObject.AllowedController.Both;
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
            if (shotsFired > shotsUntilReload)
                yield return StartCoroutine(Reload());
            else
                yield return StartCoroutine(Cooldown());
        }
    }

    public void OnTriggerReleased()
    {
       // if (VRTK_DeviceFinder.GetControllerHand(controllerEvents.gameObject) == SDK_BaseController.ControllerHand.Left)

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
