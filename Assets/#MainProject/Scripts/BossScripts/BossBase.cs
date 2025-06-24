using UnityEngine;

public class BossBase : MonoBehaviour
{
    public string bossName;
    public int bossHealth = 500;

    public float scaleSpeed = 5f;          // How fast to scale
    public float scaleDelta = 0.2f;        // How much to change per event
    public float minScaleY = 3f;
    public float minScaleZ = 3f;

    public ParticleSystem buffVFX;

    public ParticleSystem nerfVFX;

    private Vector3 targetScale;

    private void Start()
    {
        // Initialize target scale with current scale
        targetScale = transform.localScale;
    }

    private void Update()
    {
        // Smooth scale interpolation
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * scaleSpeed);
    }

    public virtual void UpdateBossUI()
    {
        Debug.Log($"{bossName} HP is now {bossHealth}");
    }

    public void ApplyDamage(int amount)
    {
        nerfVFX.Play();
        bossHealth -= amount;
        bossHealth = Mathf.Max(bossHealth, 0);
        UpdateBossUI();
        Debug.Log($"{bossName} took {amount} damage. Remaining HP: {bossHealth}");

        // Shrink smoothly (Z and Y only)
        Vector3 current = targetScale;
        current.y = Mathf.Max(minScaleY, current.y - scaleDelta);
        current.z = Mathf.Max(minScaleZ, current.z - scaleDelta);
        targetScale = new Vector3(transform.localScale.x, current.y, current.z);
    }

    public void Buff(int amount)
    {
        buffVFX.Play();
        bossHealth += amount;
        UpdateBossUI();
        Debug.Log($"{bossName} was buffed by {amount}. New HP: {bossHealth}");

        // Enlarge smoothly (Z and Y only)
        Vector3 current = targetScale;
        current.y += scaleDelta;
        current.z += scaleDelta;
        targetScale = new Vector3(transform.localScale.x, current.y, current.z);
    }
}
