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
    [SerializeField]
    private bool canSpawn = true;

    private float randomSpawnTimer;

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
        }
        else if(!waveActive)
        {
            subMarineList.Clear();
            spawnedEnemies = 0;
            StopCoroutine(spawnWave);
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
            }
        }
        for (int e = 0; e < diverCount.Count; e++)
        {
            if (diverCount[e] == null)
            {
                diverCount.RemoveAt(e);
            }
        }

        if (subMarineList.Count < waveCount / 2)
        {
            Vector2 randomPoint = Random.insideUnitCircle;
            Vector3 circleSpawn = new Vector3(randomPoint.x * 50f, -1f, randomPoint.y * 50f);

            var submarine = Instantiate(subMarine, transform.position + circleSpawn, new Quaternion(0, 0, 0, 0));

            subMarineList.Add(submarine);
            randomSpawnTimer = Random.Range(10, 50);
            spawnedEnemies++;
        }


        if (diverCount.Count < waveCount)
        {
            Vector2 randomPoint = Random.insideUnitCircle;
            Vector3 circleSpawn = new Vector3(randomPoint.x * 6f, -1f, randomPoint.y * 6f);

            var diver = Instantiate(Diver, transform.position + circleSpawn, new Quaternion(0, 0, 0, 0));

            diverCount.Add(diver);
            randomSpawnTimer = Random.Range(10, 15);
            spawnedEnemies++;
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

