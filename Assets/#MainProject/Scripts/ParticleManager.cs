using UnityEngine;
using System.Collections.Generic;

public class ParticleManager : MonoBehaviour
{
    public static ParticleManager Instance { get; private set; }

    [System.Serializable]
    public class ParticlePool
    {
        public string name;
        public GameObject prefab;
        public int poolSize;
    }

    [SerializeField]
    private List<ParticlePool> particlePools;

    private Dictionary<string, Queue<GameObject>> particlePoolDictionary;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        particlePoolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (var pool in particlePools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.poolSize; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            particlePoolDictionary.Add(pool.name, objectPool);
        }
    }

    public void SpawnParticle(string particleName, Vector3 position)
    {
        if (!particlePoolDictionary.ContainsKey(particleName))
        {
            Debug.LogWarning("Particle pool with name " + particleName + " doesn't exist.");
            return;
        }

        Queue<GameObject> particleQueue = particlePoolDictionary[particleName];

        if (particleQueue.Count == 0)
        {
            Debug.LogWarning("Particle pool with name " + particleName + " is empty. Consider increasing the pool size.");
            return;
        }

        GameObject particleToSpawn = particleQueue.Dequeue();
        particleToSpawn.transform.position = position;
        particleToSpawn.SetActive(true);

        StartCoroutine(ReplenishPool(particleName, particleToSpawn, 4.0f));
    }

    private IEnumerator<WaitForSeconds> ReplenishPool(string particleName, GameObject particle, float delay)
    {
        yield return new WaitForSeconds(delay);

        particle.SetActive(false);
        particlePoolDictionary[particleName].Enqueue(particle);
    }
}
