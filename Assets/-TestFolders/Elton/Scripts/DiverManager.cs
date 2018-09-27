﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiverManager : MonoBehaviour
{
    public GameObject Diver;
    [SerializeField]
    List<GameObject> diverCount = new List<GameObject>();

    public GameObject subMarine;
    [SerializeField]
    List<GameObject> subMarineList = new List<GameObject>();
    public GameObject helicopter;
    [SerializeField]
    List<GameObject> helicopterList = new List<GameObject>();

    [SerializeField]
    List<GameObject> enemyList = new List<GameObject>();

    [SerializeField]
    private DayAndNightCycle dayAndNight;
    private Coroutine spawnWave;
    private int spawnedEnemies;

    [SerializeField]
    private int waveCount = 1;
    public int WaveCount
    {
        get { return waveCount; }
        set { waveCount = value; }
    }
    [SerializeField]
    private bool waveActive = false;
    public bool WaveIsActive
    {
        get { return waveActive; }
        set { waveActive = value; }
    }
    [SerializeField]
    private bool canSpawn = true;
    public bool CanSpawnEnemies
    {
        get { return canSpawn; }
        set { canSpawn = value; }
    }

    private float randomSpawnTimer;
    [SerializeField]
    private int killedEnemies;

    [SerializeField]
    public GameObject player;

    [SerializeField]
    private float enemyAddition;

    public static DiverManager Instance { get; private set; }

    private bool hasStartedMusic = false;
    private bool stopMusic = false;
    private AudioController musicControl;

    private Coroutine musicRoutine;


    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        if(dayAndNight == null)
        {
            dayAndNight = FindObjectOfType<DayAndNightCycle>();
        }
        //InvokeRepeating("SpawningDivers", 10f, 7);

        //SpawningDivers();

    }

    private void Update()
    {
        if (waveActive)
        {

            Spawn();

            if(killedEnemies >= enemyAddition + waveCount * 2)
            {
                waveActive = false;
                stopMusic = true;
                StopBattleMusic();
            }
            if(dayAndNight != null)
            {
                if (dayAndNight._dayPhases == DayAndNightCycle.DayPhases.Dawn && killedEnemies < enemyAddition + waveCount * 2)
                {
                    dayAndNight.TimeMultiplier = 0;
                }
                else
                {
                    dayAndNight.TimeMultiplier = 344f * 2;
                }
                if(dayAndNight._dayPhases == DayAndNightCycle.DayPhases.Dusk)
                {
                    stopMusic = false;
                    StartBattleMusic();
                }
            }
        }
        else if(!waveActive)
        {
            subMarineList.Clear();
            helicopterList.Clear();
            diverCount.Clear();
            enemyList.Clear();
            spawnedEnemies = 0;
            killedEnemies = 0;
            if(spawnWave != null)
            {
                StopCoroutine(spawnWave);
            }
        }
    }

    public void Spawn()
    {
        if(canSpawn)
        {
            SpawnSubmarine(waveCount);
            canSpawn = false;
            spawnWave = StartCoroutine(waveCoolDown());
        }

    }

    /// <summary>
    /// Spawns amount of submarines depending on the wave.
    /// </summary>
    /// <param name="waveCount">Amount of waves player has survived</param>
    private void SpawnSubmarine(int waveCount)
    {
        for (int i = 0; i < subMarineList.Count; i++)
        {
            if (subMarineList[i] == null)
            {
                subMarineList.RemoveAt(i);
                killedEnemies = killedEnemies + 1;
            }
        }
        for (int e = 0; e < diverCount.Count; e++)
        {
            if (diverCount[e] == null)
            {
                diverCount.RemoveAt(e);
                killedEnemies = killedEnemies + 1;
            }
        }
        for (int i = 0; i < helicopterList.Count; i++)
        {
            if (helicopterList[i] == null)
            {
                helicopterList.RemoveAt(i);
                killedEnemies = killedEnemies + 1;
            }
        }

        if (enemyList.Count < enemyAddition + waveCount * 2)
    {
        if (subMarineList.Count < waveCount / 2)
        {
            Vector2 randomPoint = Random.insideUnitCircle;
            Vector3 circleSpawn = new Vector3((randomPoint.x * 50f) + 5f, -1f, (randomPoint.y * 50f) + 5f);

            var submarine = Instantiate(subMarine, transform.position + circleSpawn + new Vector3(0, -4, 0), new Quaternion(0, 0, 0, 0));
                submarine.GetComponentInChildren<SubmarineEnemy>().AimAtPlayer(player.transform);
            subMarineList.Add(submarine);
            enemyList.Add(submarine);

            spawnedEnemies++;
        }


        if (diverCount.Count < enemyAddition + waveCount)
        {
            Vector2 randomPoint = Random.insideUnitCircle;
            Vector3 circleSpawn = new Vector3(randomPoint.x * 6f +5f, -2.5f, randomPoint.y * 6f +5f);

            var diver = Instantiate(Diver, transform.position + circleSpawn, new Quaternion(0, 0, 0, 0));
            diver.GetComponent<DiverAttackers>().LookAtPlayer(player.transform);
            diverCount.Add(diver);
            enemyList.Add(diver);
            //randomSpawnTimer = Random.Range(10, 15);
            spawnedEnemies++;
        }

        if(helicopterList.Count < waveCount / 5)
        {

                Vector2 randomPoint = Random.insideUnitCircle;
                Vector3 circleSpawn = new Vector3(randomPoint.x * 60f + 10f, 30f, randomPoint.y * 60f + 10f);

                var heli = Instantiate(helicopter, transform.position + circleSpawn, new Quaternion(0, 0, 0, 0));
                heli.GetComponent<Helicopter>().FindBoat(player.transform);
                helicopterList.Add(heli);
                enemyList.Add(heli);
                //randomSpawnTimer = Random.Range(10, 15);
                spawnedEnemies++;
        }
            
            randomSpawnTimer = Random.Range(5, 20);
        }
    }

    public void StartBattleMusic()
    {
        if(!hasStartedMusic)
        {
            musicControl = GetComponent<AudioController>();
            musicControl.Play("BattleMusic", transform.position);
            GetComponentInChildren<AudioSource>().loop = true;
            hasStartedMusic = true;
        }
    }

    public void StopBattleMusic()
    {
        if (hasStartedMusic && stopMusic)
        {
            musicControl.Stop("BattleMusic");
            hasStartedMusic = false;
            stopMusic = false;
        }
    }


    IEnumerator waveCoolDown()
    {
        if(waveActive)
        {
            yield return new WaitForSeconds(randomSpawnTimer);
            canSpawn = true;
        }
    }
}

