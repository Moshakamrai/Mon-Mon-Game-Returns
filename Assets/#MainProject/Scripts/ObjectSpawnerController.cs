using UnityEngine;
using System.Collections;

public class ObjectSpawnerController : MonoBehaviour
{
    public GameObject[] objectsToSpawn;
    public Transform referenceObject; // Reference object for the spawn location
    private ObjectSpawnerModel model;

    [SerializeField] private float rightBarrier;
    [SerializeField] private float leftBarrier;

    public int catNumber;
    public float speed = 1f;
    public float spawnInterval = 3f; // Initial interval between spawns in seconds
    public float minSpawnInterval = 1.85f; // Minimum interval between spawns

    [SerializeField] private float normalTimeScale = 1.5f;
    [SerializeField] private float slowMotionTimeScale = 0.8f;
    [SerializeField] private float transitionDuration = 1f; // Duration for the smooth transition

    [SerializeField] private float maxSlowMotionDuration = 3f; // Maximum duration for slow motion
    [SerializeField] private float slowMotionRechargeTime = 0.5f; // Recharge time in seconds
    private float currentSlowMotionTime;
    private bool isRecharging = false;
    private bool isSlowMotionActive = false;

    private int objectsSpawnedCount = 0;
    private const int intervalReductionCount = 10; // Number of objects spawned before reducing spawn interval
    [SerializeField] private float intervalReductionAmount = 0.2f; // Amount to reduce spawn interval
    [SerializeField] private bool shouldSpawn;

    void Start()
    {
        model = new ObjectSpawnerModel(objectsToSpawn);
        Time.timeScale = normalTimeScale; // Set the initial time scale
        currentSlowMotionTime = maxSlowMotionDuration;
        UIManager.Instance.SetMaxSlowMotionBar(maxSlowMotionDuration);
        shouldSpawn = true;
    }

    public void GameController(bool currentSpawnStatus)
    {
        StartCoroutine(SpawnObjectPeriodically(currentSpawnStatus));
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
            // UIManager.Instance.UpdateSlowMotionBar(currentSlowMotionTime);
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
            // UIManager.Instance.UpdateSlowMotionBar(currentSlowMotionTime);
            yield return null;
        }

        currentSlowMotionTime = maxSlowMotionDuration;
        // UIManager.Instance.UpdateSlowMotionBar(maxSlowMotionDuration);
        isRecharging = false;
    }

    // Coroutine for periodic spawning
    private IEnumerator SpawnObjectPeriodically(bool spawnStatus)
    {
        while (spawnStatus)
        {
            SpawnObject();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnObject()
    {
        if (referenceObject != null && shouldSpawn)
        {
            Vector3 spawnPosition = referenceObject.position;
            spawnPosition.y -= 2f; // Adjust the Y position by 2 units
            model.SpawnNextObject(spawnPosition, null);
            AudioManager.Instance.PlaySFX2("SpawnSound");

            objectsSpawnedCount++;

            // Check if it's time to reduce spawn interval
            if (objectsSpawnedCount % intervalReductionCount == 0 && spawnInterval > minSpawnInterval)
            {
                spawnInterval -= intervalReductionAmount;
            }
        }
        else
        {
            Debug.LogWarning("Reference object not set.");
        }
    }

    public void StopSpawn()
    {
        shouldSpawn = false;
    }

    public GameObject GetNextObjectToSpawn()
    {
        return model.GetNextObjectToSpawn();
    }

   
}
