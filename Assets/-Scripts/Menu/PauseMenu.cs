using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("we got here");
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
	}

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        GameIsPaused = false;
        Time.timeScale = 1f;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        GameIsPaused = true;
        Time.timeScale = 0f;
    }

    public void LoadMenu()
    {
        Debug.Log("Loadging menu...");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
    }








}
