using UnityEngine;

public class Helicopter : MonoBehaviour, ICanTakeDamage
{
    [SerializeField] private float health;
    private float halfHealth;
    

    private bool dead;
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
    [SerializeField] private ParticleSystem particleExplotion;
    Animator anim;

    private void Start()
    {
        AudioSource[] audios = GetComponents<AudioSource>();
        audio1 = audios[0];
        audio2 = audios[1];
        audio3 = audios[2];
        halfHealth = health / 2;
        anim = gameObject.GetComponent<Animator>();
        transform.LookAt(boat.transform);
        InvokeRepeating("Shooting", anim.GetCurrentAnimatorStateInfo(0).length, 0.2f);
    }

    // TODO: Fix animation when hit and connect animations and stuff for when dead!

    public void Update()
    {
        if (!dead)
            transform.LookAt(boat.transform);
        if (dead && !sploshed)
            if (transform.position.y <= 0)
            {
                audio3.Play();
                sploshed = true;
            }
    }

    private void Shooting()
    {
        if (minigunOne.GetComponent<Minigun>().firingRate == 0 || minigunTwo.GetComponent<Minigun>().firingRate == 0)
        {
            minigunTwo.GetComponent<Minigun>().firingRate = 0.2f;
            minigunOne.GetComponent<Minigun>().firingRate = 0.2f;
        }

        Weapon shootingOne = minigunOne.GetComponent<Weapon>();
        shootingOne.Shoot();
        Weapon shootingTwo = minigunTwo.GetComponent<Weapon>();
        shootingTwo.Shoot();
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

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
            Invoke("Death", 20f);
        }
    }

    public void Death()
    {
        Destroy(gameObject);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
}