﻿using System.Collections;
using UnityEngine;
using UnityEngine.PostProcessing;

public class PlayerHealth : Health
{
    public PostProcessingProfile ppp;

    [SerializeField] private GameObject endGame;
    private Coroutine RegenHealth;

    protected override void Start()
    {
        base.Start();
        var vig = ppp.vignette.settings;
        vig.intensity = 0;
        ppp.vignette.settings = vig;
    }

    public void Update()
    {
        var vig = ppp.vignette.settings;

        if (currentHealth < maxHealth / 1.5f && currentHealth > maxHealth / 3)
        {
            vig.intensity = .66f;
        }
        if (currentHealth < maxHealth / 3)
        {
            vig.intensity = 1;
        }
        if (currentHealth > maxHealth / 1.5f)
        {
            vig.intensity = 0;
        }

        ppp.vignette.settings = vig;
    }

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
