using System.Collections;
using UnityEngine;
using VRTK;

public class Rifle : Weapon
{
    [Header("Rifle settings.")]
    public bool automatic;
    private bool isTriggerDown;
    //private GameObject controllerObject;
    //private VRTK_InteractableObject interactableObject;
    //private VRTK_ControllerEvents controllerEvents;
    //private string controllerHand;

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
