using UnityEngine;

public class Shotgun : Weapon
{
    [Header("Shotgun settings.")]
    public int numberOfBulletsToBurst;

    public override void Shoot()
    {
        if (!canFire)
            return;

        for (int i = 0; i < numberOfBulletsToBurst; i++)
            if (hitScan)
                FireWithHitScan();
            else if (!hitScan)
                FireProjectile();
        
        OnShotFired();
        ReloadAndCooldown();
    }
}
