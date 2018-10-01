using UnityEngine;

public class Helicopter : Health
{
    [SerializeField] private float health;
    [SerializeField] private float onHitEffecttime;
    private float halfHealth;
    private bool sploshed;
    private bool readyToAttack;
    AudioSource audio1;
    AudioSource audio2;
    AudioSource audio3;
    [SerializeField] private GameObject topRotor;
    [SerializeField] private GameObject botRotor;
    [SerializeField] private GameObject boat;
    [SerializeField] private GameObject minigunOne;
    [SerializeField] private GameObject minigunTwo;
    [SerializeField] private GameObject onHitparticle;
    [SerializeField] private ParticleSystem particleExplotion;
    [SerializeField] private ParticleSystem particleSplash;
    [SerializeField] private Transform muzzleParent1;
    [SerializeField] private Transform muzzleParent2;

    Minigun shootingOne;
    Minigun shootingTwo;

    Animator anim;

    protected override void Start()
    {
        base.Start();
        AudioSource[] audios = GetComponents<AudioSource>();
        audio1 = audios[0];
        audio2 = audios[1];
        audio3 = audios[2];
        halfHealth = health / 2;
        anim = gameObject.GetComponent<Animator>();
        transform.LookAt(boat.transform);
        shootingOne = minigunOne.GetComponent<Minigun>();
        shootingTwo = minigunTwo.GetComponent<Minigun>();
        InvokeRepeating("Shooting", anim.GetCurrentAnimatorStateInfo(0).length, 0.15f);
    }

    public void FindBoat(Transform player)
    {
        boat = player.gameObject;
    }

    public void Update()
    {
        if (!dead)
            transform.LookAt(boat.transform);
        if (dead && !sploshed)
            if (transform.position.y <= 0)
            {
                audio3.Play();
                sploshed = true;
                particleSplash.Play();
            }
    }

    private void Shooting()
    {
        muzzleParent1.LookAt(boat.transform);
        muzzleParent2.LookAt(boat.transform);

        shootingOne.Shoot();
        shootingTwo.Shoot();
    }

    public override void TakeDamage(float damage, Vector3 point)
    {
        health -= damage;
        audioController.Play("Hit", point);
        if (onHitparticle != null)
            Destroy(Instantiate(onHitparticle, point, Quaternion.identity), onHitEffecttime);
        if (health <= halfHealth)
        {
            topRotor.transform.parent = null;
            Rigidbody de1 = topRotor.GetComponent<Rigidbody>();
            de1.useGravity = true;
            topRotor.GetComponent<Rotator>().dead = true;
        }

        if (health <= 0 && !dead)
        {
            particleExplotion.Play();
            audio2.Play();
            anim.StopPlayback();
            anim.enabled = false;
            botRotor.GetComponent<Rotator>().dead = true;
            gameObject.GetComponent<Rigidbody>().useGravity = true;
            KillCountManager.Instance.AddKill();
            dead = true;
            audio1.Stop();
            Invoke("Death", 10f);
        }
    }

    protected override void Death()
    {
        Destroy(gameObject);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
}