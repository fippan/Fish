public class Minigun : Weapon
{
    public override void Shoot()
    {
        Fire();
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
