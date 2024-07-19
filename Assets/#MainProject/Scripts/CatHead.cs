using UnityEngine;

public class CatHead : MonoBehaviour
{
    public CatType catType;
    public bool splashSound;
    public float speed = 1f; // Speed of horizontal movement

    private bool isDragging = false;
    private Rigidbody rb;
    private Collider catCollider;
    public bool istouched;

    private void Start()
    {
        
        rb = GetComponent<Rigidbody>();
        catCollider = GetComponent<MeshCollider>();
    }

    private void Update()
    {
        HandleTouch();
    }

    private void HandleTouch()
    {
        if (Input.touchCount > 0 && !istouched)
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
                        MoveObject(touch.deltaPosition);
                    }
                    break;

                case TouchPhase.Ended:
                    isDragging = false;
                    break;
            }
        }
    }

    private void MoveObject(Vector2 deltaPosition)
    {
       // Debug.Log("cato should move");
        // Use the delta position to move the object on the x-axis
        float horizontalMovement = deltaPosition.x * speed * Time.deltaTime;
        Vector3 newPosition = transform.position + new Vector3(0f, 0f, horizontalMovement);
        transform.position = newPosition;
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject otherCat = collision.gameObject;

        // Check if the other object is a cat and has the same type
        CatHead otherCatHead = otherCat.GetComponent<CatHead>();
        if (otherCatHead != null && otherCatHead.catType == catType)
        {
            // Combine the cats using the CombinationManager
            CameraShake.Instance.ShakeCamera();
            gameObject.GetComponent<SphereCollider>().isTrigger = true;
            GameObject newCat = CombinationManager.Instance.CombineCats(gameObject, otherCat);

            if (newCat != null)
            {
                ContactPoint contact = collision.contacts[0];
                newCat.transform.position = contact.point;

                // Destroy the root parent of the old cats
                Destroy(GetRootParent(gameObject));
                AudioManager.Instance.PlaySFX2("MixSound");
                Destroy(GetRootParent(otherCat));
            }
        }
        else if (!splashSound && (collision.gameObject.CompareTag("Gato") || collision.gameObject.CompareTag("Ground")))
        {
            istouched = true;
            splashSound = true;
            AudioManager.Instance.PlaySFX2("DropSound");
            UIManager.Instance.ShowFloatingPointsSmall(collision.gameObject.transform.position, 10f);
        }
    }

    private GameObject GetRootParent(GameObject obj)
    {
        Transform root = obj.transform;
        while (root.parent != null)
        {
            root = root.parent;
        }
        return root.gameObject;
    }
}
