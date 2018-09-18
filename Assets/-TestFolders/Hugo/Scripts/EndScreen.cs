using UnityEngine;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour
{
    public Text totalGold;
    public Text daysSurvived;
    public Text enemiesKilled;

    public void GameOver ()
    {
        totalGold.text = ("Money earned: ") + CurrencyManager.Instance.GetTotalCurrency().ToString();
        daysSurvived.text = ("Days survived: ") + KillCountManager.Instance.GetKillCount();
        enemiesKilled.text = ("Enemies killed: ");

        PlayerPrefs.SetFloat("MoneyHighscore", CurrencyManager.Instance.GetTotalCurrency());
        //TODO: add playerprefs for time survived!
        PlayerPrefs.SetInt("EnemieHighscore", KillCountManager.Instance.GetKillCount());
    }

    public void Quit ()
    {
        LevelManager.Instance.MainMenu();
        Debug.Log("HENLOOOOOOOO");
    }
}
