using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class CatShaderTestScript : MonoBehaviour
{
    [SerializeField] private Material catHead;
    [SerializeField] private SkinnedMeshRenderer catBody;

    private Material runtimeMaterial;
    private Coroutine deformCoroutine;

    [SerializeField] private int maxDeform;
    [SerializeField] private int minDeform;



    void Start()
    {
        
        

    }

    private void OnEnable()
    {
        // Create a new instance of the material
        runtimeMaterial = Instantiate(catHead);

        // Assign the new material to the catBody's SkinnedMeshRenderer
        catBody.material = runtimeMaterial;
        deformCoroutine = StartCoroutine(SmoothDeformChange());

        maxDeform = 70;
        minDeform = 1;
        runtimeMaterial.SetFloat("_DeformLimit", 7f);
    }

    

    public IEnumerator SmoothDeformChange()
    {
        float duration = 0.25f; // Duration for each segment (5 times faster)
        int loopCount = 1;     // Number of loops

        for (int i = 0; i < loopCount; i++)
        {
            // Phase 1: 1 to 100
            yield return LerpDeformFloat(minDeform, maxDeform, duration);
            // Phase 2: 100 to -50
            yield return LerpDeformFloat(maxDeform, minDeform, duration);
        }

        // Ensure the value is set to the final desired end value at the end of all loops
        runtimeMaterial.SetFloat("_DeformFloat", 1f);
    }

    private IEnumerator LerpDeformFloat(float startValue, float endValue, float duration)
    {
        float time = 0f;
        while (time < duration)
        {
            // Calculate the elapsed time in the loop
            float t = time / duration;
            // Calculate the current value based on the elapsed time
            float deformValue = Mathf.Lerp(startValue, endValue, t);
            runtimeMaterial.SetFloat("_DeformFloat", deformValue);

            // Increment time
            time += Time.deltaTime;
            yield return null; // Wait for the next frame
        }
        // Ensure the value is set to the exact end value at the end of the segment
        runtimeMaterial.SetFloat("_DeformFloat", endValue);
    }
}
