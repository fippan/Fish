using System.Collections;
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

    private Transform directionTransform;


    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        if (dayAndNight == null)
        {
            dayAndNight = FindObjectOfType<DayAndNightCycle>();
        }

        directionTransform = GetComponentInChildren<Transform>();
    }

    private void Update()
    {
        if (waveActive)
        {

            Spawn();

            if (killedEnemies >= enemyAddition + waveCount * 2)
            {
                waveActive = false;
                stopMusic = true;
                StopBattleMusic();
            }
            if (dayAndNight != null)
            {
                if (dayAndNight._dayPhases == DayAndNightCycle.DayPhases.Dawn && killedEnemies < enemyAddition + waveCount * 2)
                {
                    dayAndNight.TimeMultiplier = 0;
                }
                else
                {
                    dayAndNight.TimeMultiplier = 344f * 2;
                }
                if (dayAndNight._dayPhases == DayAndNightCycle.DayPhases.Dusk)
                {
                    stopMusic = false;
                    StartBattleMusic();
                }
            }
        }
        else if (!waveActive)
        {
            subMarineList.Clear();
            helicopterList.Clear();
            diverCount.Clear();
            enemyList.Clear();
            spawnedEnemies = 0;
            killedEnemies = 0;
            if (spawnWave != null)
            {
                StopCoroutine(spawnWave);
            }
        }
    }

    public void Spawn()
    {
        if (canSpawn)
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
            if (subMarineList.Count < waveCount / 3)
            {
                Vector3 direction = GetRandomDirection();
                float distance = Random.Range(16f, 25f);

                var submarine = Instantiate(subMarine, direction * distance + new Vector3(0f, -5f, 0f), Quaternion.identity);
                submarine.GetComponentInChildren<SubmarineEnemy>().AimAtPlayer(player.transform);
                subMarineList.Add(submarine);
                enemyList.Add(submarine);

                spawnedEnemies++;
            }


            if (diverCount.Count < enemyAddition + waveCount)
            {
                Vector3 direction = GetRandomDirection();
                float distance = Random.Range(5f, 15f);

                var diver = Instantiate(Diver, direction * distance + new Vector3(0f, -2.5f, 0f), Quaternion.identity);
                diver.GetComponent<DiverAttackers>().LookAtPlayer(player.transform);
                diverCount.Add(diver);
                enemyList.Add(diver);

                spawnedEnemies++;
            }

            if (helicopterList.Count < waveCount / 4)
            {
                Vector3 direction = GetRandomDirection();
                float distance = Random.Range(16f, 25f);

                var heli = Instantiate(helicopter, direction * distance + new Vector3(0f, 30f, 0f), Quaternion.identity);
                heli.GetComponent<Helicopter>().FindBoat(player.transform);
                helicopterList.Add(heli);
                enemyList.Add(heli);

                spawnedEnemies++;
            }

            randomSpawnTimer = Random.Range(5, 20);
        }
    }

    private Vector3 GetRandomDirection()
    {
        Vector3 rotation = new Vector3(0f, Random.Range(0f, 359f), 0f);
        directionTransform.Rotate(rotation);
        Vector3 direction = directionTransform.forward;

        return direction;
    }

    public void StartBattleMusic()
    {
        if (!hasStartedMusic)
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
        if (waveActive)
        {
            yield return new WaitForSeconds(randomSpawnTimer);
            canSpawn = true;
        }
    }
}

