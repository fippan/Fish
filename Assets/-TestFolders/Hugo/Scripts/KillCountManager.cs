using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillCountManager : MonoBehaviour
{
    public int killCount = 0;

    public static KillCountManager Instance { get; private set; }

    public void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// This function should only be called if ONE enemie dies!!
    /// </summary>
    public void AddKill ()
    {
        killCount++;
    }

    public int GetKillCount ()
    {
        return killCount;
    }
}
