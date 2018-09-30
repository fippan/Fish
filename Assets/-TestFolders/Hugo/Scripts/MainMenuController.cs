using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
	public float StartDelay = 2f;
	//[Header("The name of the scene to load")]
	//public string name = "Hugo";
	[Space(20)]

	public Text moneyHighscore;
	public Text timeHighscore;
	public Text enemiesHighscore;

	private Color black = new Color(0, 0, 0);
	private bool gameHasBeenStarted = false;
	private bool gameHasBeenExited = false;

	private void Start()
	{
        if (Time.timeScale < 1)
        {
            Time.timeScale = 1;
        }
		GetHighscore();
	}

	public void GetHighscore()
	{
		moneyHighscore.text = ("Most amount of money: ") + PlayerPrefs.GetFloat("MoneyHighscore", 0).ToString();
		timeHighscore.text = ("Longest time survived: ") + PlayerPrefs.GetFloat("TimeHighscoreDays", 0).ToString() + " Days, " + PlayerPrefs.GetFloat("TimeHighscoreHours", 0).ToString() + " Hours";
		enemiesHighscore.text = ("Most enemies killed: ") + PlayerPrefs.GetInt("EnemieHighscore", 0).ToString();
	}

	public void StartGame()
	{
		if (gameHasBeenStarted)
			return;

		gameHasBeenStarted = true;
		StartCoroutine(GameStart());
		GameManager.Instance.headsetFade.Fade(black, StartDelay);
	}

	public IEnumerator GameStart()
	{
		yield return new WaitForSeconds(StartDelay + 0.1f);
		//SceneManager.LoadScene(name);

		GameManager.Instance.ChangeState(GameStates.State_InGame);
	}

	public void QuitGame()
	{
		if (gameHasBeenExited)
			return;

		gameHasBeenExited = true;

		GameManager.Instance.headsetFade.Fade(black, 1);
		StartCoroutine(Quit());
	}

	public IEnumerator Quit()
	{
		yield return new WaitForSeconds(1);
		Application.Quit();
	}
}
