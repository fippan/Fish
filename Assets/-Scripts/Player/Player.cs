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
        endGame.SetActive(true);
        DiverManager.Instance.WaveIsActive = false;
    }

    IEnumerator HealthGeneration()
    {
        yield return new WaitForSeconds(5f);
        if (currentHealth < 100)
            currentHealth += Time.deltaTime;
        else if (currentHealth > 100)
            StopCorotine();
    }
    private void StopCorotine()
    {
        if (RegenHealth != null)
            StopCoroutine(RegenHealth);
    }
}
