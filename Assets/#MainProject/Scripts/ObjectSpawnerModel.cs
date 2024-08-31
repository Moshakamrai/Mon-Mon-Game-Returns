using UnityEngine;
using System.Collections.Generic;

public class ObjectSpawnerModel
{
    private GameObject[] objectsToSpawn;
    private Dictionary<GameObject, Queue<GameObject>> objectPools; // Pool of objects

    int nextObjectIndex;

    public ObjectSpawnerModel(GameObject[] objects)
    {
        objectsToSpawn = objects;
        objectPools = new Dictionary<GameObject, Queue<GameObject>>();

        // Initialize pools
        foreach (GameObject obj in objectsToSpawn)
        {
            objectPools[obj] = new Queue<GameObject>();
        }

        SetNextObjectIndex();
    }

    public GameObject GetNextObjectToSpawn()
    {
        if (objectsToSpawn.Length == 0)
        {
            Debug.LogWarning("No objects in the array.");
            return null;
        }

        return objectsToSpawn[Random.Range(0, objectsToSpawn.Length)];
    }

    public void SpawnNextObject(Vector3 position, Transform parent)
    {
        if (objectsToSpawn.Length == 0)
        {
            Debug.LogWarning("No objects to spawn.");
            return;
        }

        GameObject objectToSpawn = GetPooledObject(objectsToSpawn[nextObjectIndex], position, parent);
        objectToSpawn.SetActive(true); // Activate the object
        SetNextObjectIndex();
    }

    private GameObject GetPooledObject(GameObject prefab, Vector3 position, Transform parent)
    {
        Queue<GameObject> pool = objectPools[prefab];

        if (pool.Count > 0)
        {
            GameObject pooledObject = pool.Dequeue();
            pooledObject.transform.position = position;
            pooledObject.transform.rotation = Quaternion.Euler(0, 180, 0);
            pooledObject.transform.parent = parent;
            return pooledObject;
        }
        else
        {
            GameObject newObj = GameObject.Instantiate(prefab, position, Quaternion.Euler(0, 180, 0), parent);
            return newObj;
        }
    }

    public void ReturnObjectToPool(GameObject obj)
    {
        Debug.Log("Returned " + obj.name);
        obj.SetActive(false);
        foreach (GameObject prefab in objectsToSpawn)
        {
            if (obj.name.Contains(prefab.name))
            {
                objectPools[prefab].Enqueue(obj);
                break;
            }
        }
    }

    private void SetNextObjectIndex()
    {
        nextObjectIndex = Random.Range(0, objectsToSpawn.Length);
    }
}
