using UnityEngine;
using System.Collections.Generic;

public class PlayerSkills : MonoBehaviour
{
    public float supernovaBlastRadius = 15f;
    public float nightVisionBlastRadius = 1f;
    public float familyBlastPower = 1f;
    public float levelAnnihilateBlastPower = 1f;
    public float verticalBlastPower = 1f;

    public List<Skill> learnedSkills = new List<Skill>();

    public void IncreaseSupernovaBoomRadius(float amount)
    {
        supernovaBlastRadius += amount;
    }

    public void IncreaseNightVisionBlastRadius(float amount)
    {
        nightVisionBlastRadius += amount;
    }

    public void IncreaseFamilyBlastPower(float amount)
    {
        familyBlastPower += amount;
    }

    public void IncreaseLevelAnnihilateBlastPower(float amount)
    {
        levelAnnihilateBlastPower += amount;
    }

    public void IncreaseVerticalBlastPower(float amount)
    {
        verticalBlastPower += amount;
    }

    private void OnEnable()
    {
        SkillEvents.Instance.OnComboMade += HandleComboMade;
    }

    private void OnDisable()
    {
        SkillEvents.Instance.OnComboMade -= HandleComboMade;
    }

    private void HandleComboMade(Vector3 position)
    {
        ActivateBoomMastery(position);
    }

    public void ActivateBoomMastery(Vector3 position)
    {
        if (CombinationManager.Instance.catCount % 8 == 0)
        {
            Debug.Log("Kaboom now at " + position);

            // Define the blast radius
            supernovaBlastRadius = 20.0f;

            // Get all colliders within the blast radius
            Collider[] colliders = Physics.OverlapSphere(position, supernovaBlastRadius);

            // Iterate over all colliders and apply the effect
            foreach (Collider collider in colliders)
            {
                // Check if the collider has a Rigidbody component
                Rigidbody rb = collider.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    // Apply an explosion force
                    float explosionForce = 4000.0f;
                    rb.AddExplosionForce(explosionForce, position, supernovaBlastRadius);
                }
                ParticleManager.Instance.SpawnParticle("Blast", position);
                CombinationManager.Instance.TriggerTemporaryTimeScaleChange();
                //
            }

            // Optionally, you can also instantiate a visual effect for the explosion
            // Example: Instantiate(explosionEffectPrefab, position, Quaternion.identity);
        }
    }


    public void ActivateShrinkChance(float chance)
    {
        // Logic for 20% chance of making blob half size
    }

    public void ActivateDetonatorFusion()
    {
        // Logic for mix with wall bounce creates a detonator
    }

    public void ActivateSpringyBlobs()
    {
        // Logic for making blobs more springy
    }

    public void ActivateRandomBurst()
    {
        // Logic for random attribute after 4 combos
    }

    public void ActivateFutureSight()
    {
        // Logic for revealing one more extra future blob
    }

    public void ActivateExtendedSlow(float timeIncrease, float rechargeRateDecrease)
    {
        // Logic for increasing slow motion time but recharging slower
    }

    public void ActivateQuickRecharge(float rechargeRateIncrease)
    {
        // Logic for slow motion recharge faster
    }

    public void ActivateSupernovaChance(float chance)
    {
        // Logic for 20% chance of supernova
    }

    public void ActivateMiniMix(int comboThreshold)
    {
        // Logic for smaller blobs after continued combos
    }

    public void ActivateComboTime(float comboTimeIncrease, float slowMotionTimeDecrease)
    {
        // Logic for increasing combo time but reducing slow motion time
    }

    public void ActivateMegaBlast(float blastSizeIncrease)
    {
        // Logic for all blasts becoming bigger
    }

    public void AddSkillPermanently(Skill skill)
    {
        if (!learnedSkills.Contains(skill))
        {
            learnedSkills.Add(skill);
            skill.ApplySkill(gameObject, Vector3.zero);
        }
    }
}
