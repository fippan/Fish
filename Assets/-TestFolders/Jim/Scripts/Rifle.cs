using System.Collections;
using UnityEngine;

public class Rifle : Weapon
{
    [Header("Rifle settings.")]
    public bool automatic;

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
