using UnityEngine;

public class Player : MonoBehaviour, ICanTakeDamage
{
    public float startingHealth = 100.0f;
	public float currentHealth;

    private void Start()
    {
        currentHealth = startingHealth;
    }

    private void Death()
    {
        //TODO: death screen/behaviour.
        //Make enemies disappear and objects non usable - activate Watch menu and choose to restart or exit game
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

		if (currentHealth <= 0)
            Death();
    }
}
