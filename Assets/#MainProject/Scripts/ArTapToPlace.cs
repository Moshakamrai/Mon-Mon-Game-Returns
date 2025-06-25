using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

public class ArTapToPlace : MonoBehaviour
{
    public GameObject gameBoardPrefab; // Assign your Suika board prefab in Inspector
    private GameObject spawnedBoard;

    private ARRaycastManager _raycastManager;
    private static List<ARRaycastHit> _hits = new List<ARRaycastHit>();

    void Start()
    {
        _raycastManager = GetComponent<ARRaycastManager>();
    }

    void Update()
    {
        // Only allow one spawn
        if (spawnedBoard != null) return;

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Vector2 touchPosition = Input.GetTouch(0).position;

            // Raycast from touch point into AR world
            if (_raycastManager.Raycast(touchPosition, _hits, TrackableType.PlaneWithinPolygon))
            {
                Pose hitPose = _hits[0].pose;

                // Instantiate game board at detected plane position
                spawnedBoard = Instantiate(gameBoardPrefab, hitPose.position, hitPose.rotation);
                spawnedBoard.SetActive(true);
            }
        }
    }
}
