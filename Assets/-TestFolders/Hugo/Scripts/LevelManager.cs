using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using VRTK;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void MainMenu ()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
