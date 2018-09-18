using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public float StartDelay = 2f;
    [Header("The name of the scene to load")]
    public string name = "Hugo";

    VRTK_HeadsetFade fade;

    private void Start()
    {
        fade = GetComponent<VRTK_HeadsetFade>();
    }

    public void StartGame ()
    {
        StartCoroutine(GameStart());
        Color black = new Color(0,0,0);
        fade.Fade(black, StartDelay);
    }

    public IEnumerator GameStart()
    {
        yield return new WaitForSeconds(StartDelay);
        SceneManager.LoadScene(name);
    }
}
