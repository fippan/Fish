﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using VRTK;

public class EndScreen : MonoBehaviour
{
    public Text totalGold;
    public Text daysSurvived;
    public Text enemiesKilled;

    public GameObject ui;

    public void GameOver ()
    {
        ui.SetActive(true);

        totalGold.text = ("Money earned: ") + CurrencyManager.Instance.GetTotalCurrency().ToString();
        daysSurvived.text = ("Days survived: ");
        enemiesKilled.text = ("Enemies killed: ") + KillCountManager.Instance.GetKillCount();

        if (PlayerPrefs.GetFloat("MoneyHighscore", 0) < CurrencyManager.Instance.GetTotalCurrency())
            PlayerPrefs.SetFloat("MoneyHighscore", CurrencyManager.Instance.GetTotalCurrency());

        //TODO: add playerprefs for time survived!
        if (PlayerPrefs.GetFloat("EnemieHighscore", 0) < KillCountManager.Instance.GetKillCount())
            PlayerPrefs.SetInt("EnemieHighscore", KillCountManager.Instance.GetKillCount());
    }

    public void Quit ()
    {
        GetComponent<VRTK_HeadsetFade>().Fade((new Color(0, 0, 0)), 2);
        StartCoroutine(Fade());
    }
    
    public IEnumerator Fade()
    {
        yield return new WaitForSeconds(2);
        LevelManager.Instance.MainMenu();
    }
}