using System.Collections;
using UnityEngine;

public class CatShaderTestScript : MonoBehaviour
{
    [SerializeField] private Material catHead;
    [SerializeField] private SkinnedMeshRenderer catBody;

    private Material runtimeMaterial;
    private Coroutine deformCoroutine;

    void Start()
    {
        Time.timeScale = 1.5f;
        // Create a new instance of the material
        runtimeMaterial = Instantiate(catHead);

        // Assign the new material to the catBody's SkinnedMeshRenderer
        catBody.material = runtimeMaterial;

        // Example of setting a float value at start (optional)
        //runtimeMaterial.SetFloat("_DeformFloat", 100f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (deformCoroutine != null)
        {
            Debug.Log("collided this");
            StopCoroutine(deformCoroutine);
        }
        deformCoroutine = StartCoroutine(SmoothDeformChange());
    }

    public IEnumerator SmoothDeformChange()
    {
        float duration = 0.3f; // Duration for each segment (5 times faster)
        int loopCount = 1;     // Number of loops

        for (int i = 0; i < loopCount; i++)
        {
            // Phase 1: 1 to 100
            yield return LerpDeformFloat(1f, 80f, duration);
            // Phase 2: 100 to -50
            yield return LerpDeformFloat(80f, 1f, duration);
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
