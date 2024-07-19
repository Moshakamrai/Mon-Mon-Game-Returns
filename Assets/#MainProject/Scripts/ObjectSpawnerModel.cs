using UnityEngine;

public class ObjectSpawnerModel
{
    private GameObject[] objectsToSpawn;
    private int nextObjectIndex;

    public ObjectSpawnerModel(GameObject[] objects)
    {
        objectsToSpawn = objects;
        SetNextObjectIndex();
    }

    public GameObject GetNextObjectToSpawn()
    {
        if (objectsToSpawn.Length == 0)
        {
            Debug.LogWarning("No objects in the array.");
            return null;
        }
        //Debug.Log(objectsToSpawn[nextObjectIndex]);
        return objectsToSpawn[nextObjectIndex];
    }

    public void SpawnNextObject(Vector3 position, Transform parent)
    {
        if (objectsToSpawn.Length == 0)
        {
            Debug.LogWarning("No objects to spawn.");
            return;
        }
        GameObject.Instantiate(objectsToSpawn[nextObjectIndex], position, Quaternion.Euler(0, 180, 0), parent);
        SetNextObjectIndex();
    }

    private void SetNextObjectIndex()
    {
        
        nextObjectIndex = Random.Range(0, objectsToSpawn.Length);
        //UIManager.Instance.SetNextBlobUI(nextObjectIndex);
    }
}
