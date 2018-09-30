using System;
using System.Collections;
using UnityEngine;

public class SlowMotion : MonoBehaviour
{
    public static SlowMotion Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }

    public void StartSlowMotion(float duration)
    {
        StartCoroutine(SlowMotionTimer(duration));
    }

    public void StartSlowMotion(float duration, float delay)
    {
        StartCoroutine(SlowMotionTimer(duration, delay));
    }

    private IEnumerator SlowMotionTimer(float duration, float delay = 0)
    {
        float myTime = 0;
        while (myTime < delay)
        {
            myTime += Time.unscaledDeltaTime;
            yield return null;
        }
        Time.timeScale = 0.05f;
        myTime = 0;
        while (myTime < duration)
        {
            myTime += Time.unscaledDeltaTime;
            yield return null;
        }

        Time.timeScale = 1;
    }
}
