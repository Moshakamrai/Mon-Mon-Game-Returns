using UnityEngine;

public class CatHead : MonoBehaviour
{
    public CatType catType;
    


    private void Start()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        GameObject otherCat = collision.gameObject;
        CameraShake.Instance.ShakeCamera();
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

                //newCat.transform.position += Vector3.down * 3f;

                // Destroy the root parent of the old cats
                Destroy(GetRootParent(gameObject));
                Destroy(GetRootParent(otherCat));

                // Notify observers about the new cat type
                // FindObjectOfType<GameManager>().gameSubject.NotifyCatCombined(newCat.GetComponent<CatHead>().catType);
            }
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
