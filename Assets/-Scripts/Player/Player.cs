using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, ICanTakeDamage
{
    [SerializeField] private float startingHealth;
    [SerializeField] private float currentHealth;
    [SerializeField] private GameObject endGame;
    private Coroutine RegenHealth;

    private void Start()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage(float amount)
    {
        StopCorotine();
        currentHealth -= amount;

        if (currentHealth <= 0)
            Death();

        RegenHealth = StartCoroutine(HealthGeneration());
    }

    private void Death()
    {
        endGame.GetComponent<EndScreen>().GameOver();
        DiverManager.Instance.WaveIsActive = false;
    }

    IEnumerator HealthGeneration()
    {
        yield return new WaitForSeconds(5f);
        while (currentHealth < startingHealth)
        {
            currentHealth += Time.deltaTime;

            if (currentHealth >= startingHealth)
            {
                currentHealth = startingHealth;
                break;
            }
            yield return null;
        }
    }
    private void StopCorotine()
    {
        if (RegenHealth != null)
            StopCoroutine(RegenHealth);
    }
}
