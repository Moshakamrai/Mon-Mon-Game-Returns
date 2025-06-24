using UnityEngine;

public class BossBlobLauncher : MonoBehaviour
{
    public float launchForce = 15f;
    public float upwardBoost = 1.2f;
    public bool isActive = true;  // <-- Toggle this when boss is defeated

    private void OnCollisionEnter(Collision collision)
    {
        if (!isActive) return;
        
        
        Rigidbody rb = collision.rigidbody;

        if (rb != null && !rb.isKinematic)
        {
            ContactPoint contact = collision.contacts[0];
            Vector3 reflectDir = Vector3.Reflect(rb.velocity.normalized, contact.normal);

            Vector3 launchDir = reflectDir + Vector3.up * upwardBoost;
            ParticleManager.Instance.SpawnParticle("BossNerf1", collision.contacts[0].point);
            rb.velocity = Vector3.zero;
            rb.AddForce(launchDir.normalized * launchForce, ForceMode.Impulse);
            Debug.LogError($"Launching {collision.gameObject.name}!");
        }
    }


    // Optional: Method to disable this from boss script
    public void DisableLaunch()
    {
        isActive = false;
    }
}
