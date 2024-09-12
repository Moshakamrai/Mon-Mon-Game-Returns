using UnityEngine;
using System.Collections;

public class ObjectSpawnerController : MonoBehaviour
{
    public GameObject[] objectsToSpawn;
    public GameObject Oddobject1;
    public Transform referenceObject; // Reference object for the spawn location

    private int nextObjectIndex;

    [SerializeField] private float rightBarrier;
    [SerializeField] private float leftBarrier;

    public int catNumber;
    public float speed = 1f;
    public float spawnInterval = 3f; // Initial interval between spawns in seconds
    public float minSpawnInterval = 1.85f; // Minimum interval between spawns

    [SerializeField] private float normalTimeScale = 1.5f;
    [SerializeField] private float slowMotionTimeScale = 0.8f;

    [SerializeField] private float maxSlowMotionDuration = 3f; // Maximum duration for slow motion
    [SerializeField] private float slowMotionRechargeTime = 0.5f; // Recharge time in seconds
    private float currentSlowMotionTime;
    private bool isRecharging = false;
    private bool isSlowMotionActive = false;

    private int objectsSpawnedCount = 0;
    [SerializeField] private bool shouldSpawn;

    private Coroutine spawnCoroutine; // Reference to the coroutine
    private bool isSpawning = false;  // Flag to ensure only one coroutine runs at a time

    void Start()
    {
        SetNextObjectIndex();

        Time.timeScale = normalTimeScale; // Set the initial time scale
        currentSlowMotionTime = maxSlowMotionDuration;
        UIManager.Instance.SetMaxSlowMotionBar(maxSlowMotionDuration);
        shouldSpawn = true;
        GameController(true);
    }

    public void GameController(bool currentSpawnStatus)
    {
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null; // Reset coroutine reference
        }
        if (currentSpawnStatus)
        {
            spawnCoroutine = StartCoroutine(SpawnObjectPeriodically());
        }
    }

    void Update()
    {
        HandleTouch();
    }

    void HandleTouch()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    TriggerSlowMotion();
                    break;

                case TouchPhase.Ended:
                    ResetTimeScale();
                    break;
            }
        }
    }

    // Function to trigger slow motion
    public void TriggerSlowMotion()
    {
        if (currentSlowMotionTime > 0 && !isRecharging)
        {
            isSlowMotionActive = true;
            Time.timeScale = slowMotionTimeScale;
            Time.fixedDeltaTime = Time.timeScale * 0.01f;
            UIManager.Instance.StartDecreaseSlowMotionBar(); // Start decreasing slider value
            StartCoroutine(SlowMotionDuration());
        }
    }

    // Function to reset to normal time scale
    public void ResetTimeScale()
    {
        isSlowMotionActive = false;
        Time.timeScale = normalTimeScale;
        UIManager.Instance.StartIncreaseSlowMotionBar(); // Start increasing slider value
        StartCoroutine(RechargeSlowMotion());
    }

    // Coroutine to handle slow motion duration
    private IEnumerator SlowMotionDuration()
    {
        while (isSlowMotionActive && currentSlowMotionTime > 0)
        {
            currentSlowMotionTime -= Time.unscaledDeltaTime;
            yield return null;
        }

        ResetTimeScale();
        StartCoroutine(RechargeSlowMotion());
    }

    // Coroutine to recharge slow motion time
    private IEnumerator RechargeSlowMotion()
    {
        isRecharging = true;
        while (currentSlowMotionTime < maxSlowMotionDuration)
        {
            currentSlowMotionTime += slowMotionRechargeTime * Time.unscaledDeltaTime;
            yield return null;
        }

        currentSlowMotionTime = maxSlowMotionDuration;
        isRecharging = false;
    }

    // Coroutine for periodic spawning
    private IEnumerator SpawnObjectPeriodically()
    {
        isSpawning = true;
        while (shouldSpawn)
        {
            SpawnObject();
            yield return new WaitForSeconds(spawnInterval);
        }
        isSpawning = false;
    }

    void SpawnObject()
    {
        if (referenceObject != null && shouldSpawn)
        {
            Vector3 spawnPosition = referenceObject.position;
            spawnPosition.y -= 2f; // Adjust the Y position by 2 units
            GameObject objectToSpawn = Instantiate(objectsToSpawn[nextObjectIndex], spawnPosition, Quaternion.Euler(0, 180, 0));
            objectToSpawn.SetActive(true); // Activate the object
            AudioManager.Instance.PlaySFX2("SpawnSound");

            objectsSpawnedCount++;

            SetNextObjectIndex();
        }
        else
        {
            Debug.LogWarning("Reference object not set.");
        }
    }

    private void SetNextObjectIndex()
    {
        nextObjectIndex = Random.Range(0, objectsToSpawn.Length);
    }

    public void StopSpawn()
    {
        shouldSpawn = false;
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine); // Stop the existing coroutine
            spawnCoroutine = null; // Reset the coroutine reference
        }
    }

    public void StartSpawn()
    {
        if (!shouldSpawn)
        {
            shouldSpawn = true;
            if (!isSpawning)
            {
                spawnCoroutine = StartCoroutine(SpawnObjectPeriodically());
            }
        }
    }

    public GameObject GetNextObjectToSpawn()
    {
        return objectsToSpawn[nextObjectIndex];
    }

    public void RedirectRemoveBlob(GameObject obj)
    {
        Destroy(obj);
    }
}
