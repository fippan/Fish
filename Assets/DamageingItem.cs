using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageingItem : MonoBehaviour
{
    public float damage = 0f;
    public bool canNotDamagePlayer = false;

    ICanTakeDamage takeDamage;

    public void OnTriggerEnter(Collider other)
    {
        ICanTakeDamage takeDamage = other.gameObject.GetComponentInParent<ICanTakeDamage>();
        if (takeDamage != null)
        {
            if (canNotDamagePlayer)
            {
                if (other.gameObject.tag == "Player")
                {
                    return;
                }
                else
                {
                takeDamage.TakeDamage(damage);
                }
            }
            else
            {
                takeDamage.TakeDamage(damage);
            }
        }
    }
}
