public class Pistol : Weapon
{
    public override void Shoot()
    {
        if (!canFire)
            return;

        if (hitScan)
            FireWithHitScan();
        else if (!hitScan)
            FireProjectile();

        OnShotFired();
        ReloadAndCooldown();
    }
}
