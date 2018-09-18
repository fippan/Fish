using UnityEngine;

public class Helicopter : MonoBehaviour, ICanTakeDamage
{
    [SerializeField] private float health;

    private bool dead;
    private bool readyToAttack;

    [SerializeField] private GameObject topRotor;
    [SerializeField] private GameObject boat;
    [SerializeField] private GameObject minigunOne;
    [SerializeField] private GameObject minigunTwo;

    private void Start()
    {
        Animator anim = gameObject.GetComponent<Animator>();
        transform.LookAt(boat.transform);
        minigunOne.GetComponent<Rifle>().firingRate = 0.2f;
        minigunTwo.GetComponent<Rifle>().firingRate = 0.2f;
    }

     // TODO: Fix so that it starts firing after first animation is done. Fix animation when hit and connect animations and stuff for when dead!

    public void Update()
    {
        if (readyToAttack)
        {
            transform.LookAt(boat.transform);

        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 50)
        {
            topRotor.transform.parent = null;
            Rigidbody de1 = topRotor.GetComponent<Rigidbody>();
            de1.useGravity = true;
        }

        else if (health <= 0)
        {
            Death();
        }
    }
    public void Death()
    {
        dead = true;
        Destroy(gameObject);
    }
}
