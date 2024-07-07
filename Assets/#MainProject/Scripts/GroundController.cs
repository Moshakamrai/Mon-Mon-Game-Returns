using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionExit(Collision collision)
    {
        // Check if the collision object's layer is "GatoLayer"
        if (collision.gameObject.layer == LayerMask.NameToLayer("GatoLayer"))
        {
            // Get the root parent object

            // Perform your action here
            collision.gameObject.GetComponent<CatHead>().splashSound = false;
           
        }
    }

    // Helper method to get the root parent of a given transform
   
}
