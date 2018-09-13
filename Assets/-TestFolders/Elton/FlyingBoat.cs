using UnityEngine;

public class FlyingBoat : MonoBehaviour , ICanTakeDamage{
    public float health;
    public bool dead;

    public GameObject detachable1;
    public GameObject detachable2;
    public GameObject detachable3;

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 75)
        {
            detachable1.transform.parent = null;
            Rigidbody de1 = detachable1.GetComponent<Rigidbody>();
            de1.useGravity = true;
        }

        else if (health <= 50)
        {
            detachable2.transform.parent = null;
            Rigidbody de2 = detachable2.GetComponent<Rigidbody>();
            de2.useGravity = true;
        }

        else if (health <= 25)
        {
            detachable3.transform.parent = null;
            Rigidbody de3 = detachable3.GetComponent<Rigidbody>();
            de3.useGravity = true;
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