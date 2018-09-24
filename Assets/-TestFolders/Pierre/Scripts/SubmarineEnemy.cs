using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmarineEnemy : Enemy
{

    [SerializeField]
    public bool canFire;
    [SerializeField]
    protected Transform playerTransform;
    [SerializeField]
    protected Transform muzzleTransform;
    [SerializeField]
    protected GameObject bulletFX;
    [SerializeField]
    protected AudioClip fireSound;

    private AudioSource audioSource;

    private Coroutine shootBehave;

    public Vector2 minSpreadDegrees;
    public Vector2 maxSpreadDegrees;

    private Transform player;

    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();
        //transform.LookAt(playerTransform);
        //transform.rotation *= Quaternion.Euler(0, -30, 0);
    }
	
    public void AimAtPlayer(Transform player)
    {
        this.player = player;
        //transform.LookAt(new Vector3(player.position.x+40f, transform.position.y, player.position.z +40f));
        //muzzleTransform.LookAt(new Vector3(player.position.x, muzzleTransform.position.y, player.position.z));
    }

	// Update is called once per frame
	void Update () {
        Fire();
	}

    public void Fire()
    {
        if(canFire)
        {
            muzzleTransform.LookAt(player);
            Shoot();
            canFire = false;
            shootBehave = StartCoroutine(ShootBehaviour());
        }
    }
    public void Shoot()
    {
        audioSource.clip = fireSound;
        audioSource.Play();
        Quaternion rotation = CalculateProjectileRotation();
        var Bullet = Instantiate(bulletFX, muzzleTransform.position, rotation);
        Destroy(Bullet, 2f);
    }

    public IEnumerator ShootBehaviour()
    {
        yield return new WaitForSeconds(1.2f);
        canFire = true;
    }

    private Quaternion CalculateProjectileRotation()
    {
        Quaternion barrelEndStartRotation = muzzleTransform.rotation;
        Vector3 newRotation = new Vector3(
            Random.Range(minSpreadDegrees.x, maxSpreadDegrees.x),
            Random.Range(minSpreadDegrees.y, maxSpreadDegrees.y),
            0f);
        muzzleTransform.Rotate(newRotation);
        Quaternion rotation = muzzleTransform.rotation;
        muzzleTransform.rotation = barrelEndStartRotation;

        return rotation;
    }
}
