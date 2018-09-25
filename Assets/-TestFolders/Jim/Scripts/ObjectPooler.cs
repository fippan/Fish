using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance;

    private Dictionary<string, GameObject> prefabs = new Dictionary<string, GameObject>();
    private Dictionary<string, List<GameObject>> pooledObjects = new Dictionary<string, List<GameObject>>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this);
        }
    }

    public void AddPool(string key, GameObject prefab, int count)
    {
        if (prefabs.ContainsKey(key))
            return;

        //Stores the prefab so we can create new objects if pool runs out.
        prefabs.Add(key, prefab);

        List<GameObject> newList = new List<GameObject>();
        for (int i = 0; i < count; i++)
        {
            GameObject newGameObject = Instantiate(prefab);
            newGameObject.SetActive(false);
            newList.Add(newGameObject);
        }
        pooledObjects.Add(key, newList);
    }

    public GameObject GetPooledObject(string key)
    {
        if (!pooledObjects.ContainsKey(key))
        {
            Debug.LogWarning("Key do not exist!");
            return null;
        }

        //Returns first available object in pool.
        foreach (GameObject item in pooledObjects[key])
        {
            if (!item.activeInHierarchy)
            {
                return item;
            }
        }

        //If no available objects in pool. Create a new one, add it to the pool and returns it.
        GameObject newGameObject = Instantiate(prefabs[key]);
        newGameObject.SetActive(false);
        pooledObjects[key].Add(newGameObject);
        return newGameObject;
    }
}
