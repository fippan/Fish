using UnityEngine;

public class PlayerHealth : MonoBehaviour, ICanTakeDamage
{
    public float startingHealth = 100;
    public float currentHealth;

    void Awake()
    {
        currentHealth = startingHealth;
    }

    void Death()
    {
        //TODO: death screen/behaiviour.
        //Make enemies dissapear and objects non usable - activate Watch menu and choose to restart or exit game
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Death();
        }
    }
}
