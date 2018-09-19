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

    public static DiverManager Instance { get; private set; }


    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        //InvokeRepeating("SpawningDivers", 10f, 7);

        //SpawningDivers();

    }

    private void Update()
    {
        if (waveActive)
        {

            Spawn();

            if(killedEnemies >= waveCount * 2)
            {
                waveActive = false;
            }
        }
        else if(!waveActive)
        {
            subMarineList.Clear();
            helicopterList.Clear();
            diverCount.Clear();
            enemyList.Clear();
            spawnedEnemies = 0;
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

    //public int CheckIfKilled()
    //{
    //    return 
    //}

    /// <summary>
    /// Spawns divers around the boat in a random manner
    /// </summary>
    void SpawningDivers(int waveCount)
    {
        //for (int e = 0; e < diverCount.Count; e++)
        //{
        //    if (diverCount[e] == null)
        //    {
        //        diverCount.RemoveAt(e);
        //    }
        //}
        //if (diverCount.Count < waveCount)
        //{
        //    Vector2 randomPoint = Random.insideUnitCircle;
        //    Vector3 circleSpawn = new Vector3(randomPoint.x * 6f, -1f, randomPoint.y * 6f);
            
        //    var diver = Instantiate(Diver,transform.position + circleSpawn, new Quaternion(0, 0, 0, 0));

        //    diverCount.Add(diver);
        //    randomSpawnTimer = Random.Range(10, 50);
        //    spawnedEnemies++;
        //}
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

        if (enemyList.Count < waveCount * 2)
    {
        if (subMarineList.Count < waveCount)
        {
            Vector2 randomPoint = Random.insideUnitCircle;
            Vector3 circleSpawn = new Vector3(randomPoint.x * 50f, -1f, randomPoint.y * 50f);

            var submarine = Instantiate(subMarine, transform.position + circleSpawn + new Vector3(0, -4, 0), new Quaternion(0, 0, 0, 0));

            subMarineList.Add(submarine);
            enemyList.Add(submarine);
            randomSpawnTimer = Random.Range(10, 50);
            spawnedEnemies++;
        }


        if (diverCount.Count < waveCount)
        {
            Vector2 randomPoint = Random.insideUnitCircle;
            Vector3 circleSpawn = new Vector3(randomPoint.x * 6f, -1f, randomPoint.y * 6f);

            var diver = Instantiate(Diver, transform.position + circleSpawn, new Quaternion(0, 0, 0, 0));

            diverCount.Add(diver);
            enemyList.Add(diver);
            //randomSpawnTimer = Random.Range(10, 15);
            spawnedEnemies++;
        }

        if(helicopterList.Count < waveCount / 5)
        {
                var heli = Instantiate(helicopter, transform.position + new Vector3(50, 30, 50), new Quaternion(0, 0, 0, 0));

                helicopterList.Add(heli);
                enemyList.Add(heli);
                //randomSpawnTimer = Random.Range(10, 15);
                spawnedEnemies++;
            }
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

