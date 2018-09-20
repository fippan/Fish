using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public float StartDelay = 2f;
    [Header("The name of the scene to load")]
    public string name = "Hugo";
    [Space(20)]

    public Text moneyHighscore;
    public Text timeHighscore;
    public Text enemiesHighscore;

    VRTK_HeadsetFade fade;
    Color black = new Color(0, 0, 0);

    private void Start()
    {
        fade = GetComponent<VRTK_HeadsetFade>();
        GetHighscore();
    }

    public void GetHighscore ()
    {
        moneyHighscore.text = ("Most amount of money: ") + PlayerPrefs.GetFloat("MoneyHighscore", 0).ToString();
        timeHighscore.text = ("Longest time survived: ") + PlayerPrefs.GetFloat("TimeHighscore", 0).ToString();
        enemiesHighscore.text = ("Most enemies killed: ") + PlayerPrefs.GetInt("EnemieHighscore", 0).ToString();
    }

    public void StartGame ()
    {
        StartCoroutine(GameStart());
        fade.Fade(black, StartDelay);
    }

    public IEnumerator GameStart()
    {
        yield return new WaitForSeconds(StartDelay);
        SceneManager.LoadScene(name);
    }

    public void QuitGame ()
    {
        fade.Fade(black, 1);
        StartCoroutine(Quit());
    }

    public IEnumerator Quit()
    {
        yield return new WaitForSeconds(1);
        Application.Quit();
    }
}
