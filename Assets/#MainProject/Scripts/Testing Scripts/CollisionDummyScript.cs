using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDummyScript : MonoBehaviour
{
    public CatShaderTestScript testScript;
    private void OnCollisionEnter(Collision collision)
    {
        StartCoroutine(testScript.SmoothDeformChange());
    }
}
