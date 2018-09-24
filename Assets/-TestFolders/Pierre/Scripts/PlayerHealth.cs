using System.Collections;
using UnityEngine;

public class PlayerHealth : Health
{
    [SerializeField] private GameObject endGame;
    private Coroutine RegenHealth;

    public override void TakeDamage(float amount, Vector3 point)
    {
        StopCorotine();
        base.TakeDamage(amount, point);
        RegenHealth = StartCoroutine(HealthGeneration());
    }

    protected override void Death()
    {
        endGame.GetComponent<EndScreen>().GameOver();
        DiverManager.Instance.WaveIsActive = false;
    }

    private IEnumerator HealthGeneration()
    {
        yield return new WaitForSeconds(5f);
        while (currentHealth < maxHealth)
        {
            currentHealth += Time.deltaTime;

            if (currentHealth >= maxHealth)
            {
                currentHealth = maxHealth;
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
