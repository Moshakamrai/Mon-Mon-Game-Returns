using UnityEngine;

public class ObjectSpawnerController : MonoBehaviour
{
    public GameObject[] objectsToSpawn;
    public Transform referenceObject; // Reference object for the spawn location
    private ObjectSpawnerModel model;
    private bool isDragging = false;

    [SerializeField] private float rightBarrier;
    [SerializeField] private float leftBarrier;

    public float speed = 1f;

    void Start()
    {
        model = new ObjectSpawnerModel(objectsToSpawn);
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
                    isDragging = true;
                    break;

                case TouchPhase.Moved:
                    if (isDragging)
                    {
                        MoveReferenceObject(touch.deltaPosition);
                    }
                    break;

                case TouchPhase.Ended:
                    if (isDragging)
                    {
                        SpawnObject();
                        isDragging = false;
                    }
                    break;
            }
        }
    }

    void MoveReferenceObject(Vector2 deltaPosition)
    {
        // Use the delta position to move the reference object on the z-axis
        float horizontalMovement = deltaPosition.x * speed * Time.deltaTime;
        float newZPosition = referenceObject.position.z + horizontalMovement;

        // Ensure the reference object stays within the left and right barriers
        newZPosition = Mathf.Clamp(newZPosition, leftBarrier, rightBarrier);

        // Update the reference object's position
        referenceObject.position = new Vector3(referenceObject.position.x, referenceObject.position.y, newZPosition);

        // Update the spawner's position to follow the reference object
        transform.position = referenceObject.position;
    }

    void SpawnObject()
    {
        //GetNextObjectToSpawn();
        if (referenceObject != null)
        {
            Vector3 spawnPosition = referenceObject.position;
            spawnPosition.y -= 2f; // Adjust the Y position by 2 units
            model.SpawnNextObject(spawnPosition, null);
        }
        else
        {
            Debug.LogWarning("Reference object not set.");
        }
    }

    public GameObject GetNextObjectToSpawn()
    {
        
        return model.GetNextObjectToSpawn();
    }
}
