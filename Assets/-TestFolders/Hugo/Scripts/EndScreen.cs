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
        daysSurvived.text = ("Days survived: ");
        enemiesKilled.text = ("Enemies killed");
    }

    public void Quit ()
    {
        LevelManager.Instance.MainMenu();
        Debug.Log("HENLOOOOOOOO");
    }
}
