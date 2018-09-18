using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiverManager : MonoBehaviour
{
    public GameObject Diver;
    List<GameObject> diverCount = new List<GameObject>();

    public GameObject subMarine;
    List<GameObject> subMarineList = new List<GameObject>();

    private void Start()
    {
        //InvokeRepeating("SpawningDivers", 10f, 7);

        //SpawningDivers();
        SpawnSubmarine();
    }

    /// <summary>
    /// Spawns divers around the boat in a random manner
    /// </summary>
    void SpawningDivers()
    {
        for (int e = 0; e < diverCount.Count; e++)
        {
            if (diverCount[e] == null)
            {
                diverCount.RemoveAt(e);
            }
        }
        if (diverCount.Count < 4)
        {
            Vector2 randomPoint = Random.insideUnitCircle;
            Vector3 circleSpawn = new Vector3(randomPoint.x * 6f, -1f, randomPoint.y * 6f);
            
            var diver = Instantiate(Diver,transform.position + circleSpawn, new Quaternion(0, 0, 0, 0));

            diverCount.Add(diver);
        }
    }

    void SpawnSubmarine()
    {
        for (int i = 0; i < subMarineList.Count; i++)
        {
            if (subMarineList[i] == null)
            {
                subMarineList.RemoveAt(i);
            }
        }
        if (subMarineList.Count < 4)
        {
            Vector2 randomPoint = Random.insideUnitCircle;
            Vector3 circleSpawn = new Vector3(randomPoint.x * 50f, -1f, randomPoint.y * 50f);

            var submarine = Instantiate(subMarine, transform.position + circleSpawn, new Quaternion(0, 0, 0, 0));

            subMarineList.Add(submarine);
        }
    }
}

