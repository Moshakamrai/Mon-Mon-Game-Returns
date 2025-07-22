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

    private ObjectSpawnerModel model;
    

    [SerializeField] private float mouseSpeedMultiplier = 10f;
    [SerializeField] private float touchSpeedMultiplier = 10f;

    private void Start()
    {

        rb = GetComponent<Rigidbody>();
        catCollider = GetComponent<MeshCollider>();
        speed = 30f;
    }

    private void Update()
{
    HandleInput();
}

private void HandleInput()
{
    // Touch Input (Mobile or WebGL touch screens)
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
                    // Scale down touch movement (e.g., divide by screen width for consistency)
                    Vector2 delta = touch.deltaPosition / Screen.dpi * touchSpeedMultiplier / 12;
                    MoveObject(delta);
                }
                break;

            case TouchPhase.Ended:
            case TouchPhase.Canceled:
                isDragging = false;
                break;
        }
    }
    // Mouse Input (PC or WebGL desktop)
    else if (!istouched)
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
        }
        else if (Input.GetMouseButton(0) && isDragging)
        {
            Vector2 delta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) * mouseSpeedMultiplier;
            MoveObject(delta);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
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
            
            gameObject.GetComponent<SphereCollider>().isTrigger = true;
            GameObject newCat = CombinationManager.Instance.CombineCats(gameObject, otherCat);

            if (newCat != null)
            {
                ContactPoint contact = collision.contacts[0];
                newCat.transform.position = contact.point;
                CameraShake.Instance.ShakeCamera();
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
   
   public void CatShrink()
   {
       GameObject currentCat = GetRootParent(gameObject);
       
       Vector3 newScale = currentCat.transform.localScale;
       newScale.y /= 2;
       newScale.z /= 2;
   
       // Ensure the scale values do not go below zero
       newScale.y = Mathf.Max(newScale.y, 0.1f);
       newScale.z = Mathf.Max(newScale.z, 0.1f);
   
       currentCat.transform.localScale = newScale;
   }


}
