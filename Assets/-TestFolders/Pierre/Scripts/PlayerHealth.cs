using System.Collections;
using UnityEngine;
using UnityEngine.PostProcessing;

public class PlayerHealth : Health
{
    public PostProcessingProfile ppp;

    [SerializeField] private GameObject endGame;
    private Coroutine RegenHealth;

    public override void TakeDamage(float amount, Vector3 point)
    {
        var vig = ppp.vignette.settings;
        vig.intensity = 1;

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
