using UnityEngine;
using System.Collections.Generic;


public class PlayerSkills : MonoBehaviour
{
    public float supernovaBlastRadius = 15f;
    public float nightVisionBlastRadius = 1f;
    public float familyBlastPower = 1f;
    public float levelAnnihilateBlastPower = 1f;
    public float BlastWidthVerticle = 5f;
    public float BlastWidthHorizontal = 5f;
    public float verticalBlastDepth = 10f; // Adjust as needed
    public float horizontalBlastDepth = 10f;
    public List<Skill> learnedSkills = new List<Skill>();

    public SkillManager skillManageScript;

   // public string skillName;

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
        BlastWidthVerticle += amount;
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
        ActivateVerticalBoom(position);
        ActivateHorizontalBoom(position);
        ActivateTinyPawsensibility(position);
        ActivateFurNovaBlast(position);
        

        if (SkillEvents.Instance.comboCounter % 3 == 0)
        {
            ActivateMiniMix(position);
            ActivateRandomBurst(position);
            ActivateComboNovaBlast(position); 
        }
        if (SkillEvents.Instance.comboCounter % 4 == 0)
        {
            ActivateComboNovaBlast(position); 
            ActivateGatoComboStrike(position);
            ActivateLightningComboSurge(position);
        }
        if (SkillEvents.Instance.comboCounter % 5 == 0)
        {
            ActivateLightningComboSurge(position);
        }
    }



    public void ActivateBoomMastery(Vector3 position)
    {
        BoomMastery supernovaBoomSkill = (BoomMastery)learnedSkills.Find(skill => skill is BoomMastery);
        if (supernovaBoomSkill != null)
        {
            
            float mixCount = supernovaBoomSkill.mixCount;
            if (CombinationManager.Instance.catCount % mixCount == 0)
            {
                CameraShake.Instance.ShakeCamera2();
                BoomMastery(position);
                // Optionally, you can also instantiate a visual effect for the explosion
                // Example: Instantiate(explosionEffectPrefab, position, Quaternion.identity);
            }
        }
    }


    public void ActivateVerticalBoom(Vector3 position)
    {
        VerticalBoom verticalBoomSkill = (VerticalBoom)learnedSkills.Find(skill => skill is VerticalBoom);
        if (verticalBoomSkill != null)
        {
           
            float mixCount = verticalBoomSkill.mixCount;
            if (CombinationManager.Instance.catCount % mixCount == 0)
            {
                CameraShake.Instance.ShakeCamera2();
                VerticleBoom(position);
            }
        }
    }

    public void ActivateHorizontalBoom(Vector3 position)
    {
        HorizontalBoom horizontalBoomSkill = (HorizontalBoom)learnedSkills.Find(skill => skill is HorizontalBoom);
        if (horizontalBoomSkill != null)
        {
            
            float mixCount = horizontalBoomSkill.mixCount;
            if (CombinationManager.Instance.catCount % mixCount == 0)
            {
               CameraShake.Instance.ShakeCamera2();
               HorizontalBoom(position);
            }
        }
    }

    public void ActivateComboNovaBlast(Vector3 position)
    {
        ComboNovaBlast BoomSkill = (ComboNovaBlast)learnedSkills.Find(skill => skill is ComboNovaBlast);
        if (BoomSkill != null)
        {
            BoomMastery(position);  
        }
    }

    public void ActivateGatoComboStrike(Vector3 position)
    {
        GatoComboStrike BoomSkill = (GatoComboStrike)learnedSkills.Find(skill => skill is GatoComboStrike);
        if (BoomSkill != null)
        {
            HorizontalBoom(position);  
        }
    }

    public void ActivateLightningComboSurge(Vector3 position)
    {
        LightningComboSurge BoomSkill = (LightningComboSurge)learnedSkills.Find(skill => skill is LightningComboSurge);
        if (BoomSkill != null)
        {
            VerticleBoom(position);
        }
        
    }


    public void ActivateTinyPawsensibility(Vector3 position)
    {
        ActivateTinyPawsensibility BoomSkill = (ActivateTinyPawsensibility)learnedSkills.Find(skill => skill is ActivateTinyPawsensibility);
        if (BoomSkill != null)
        {
            if (CombinationManager.Instance.ChanceCalculation(20))
            {
                GameObject currentCato = CombinationManager.Instance.lastMixCato;
                currentCato.GetComponentInChildren<CatHead>().CatShrink();
                ParticleManager.Instance.SpawnParticle("ShrinkEffect", currentCato.transform.position);
                CombinationManager.Instance.TriggerTemporaryTimeScaleChange(1.4f);
            }   
        }
        
    }

    public void ActivateFurNovaBlast(Vector3 position)
    {
        FurNovaBlast BoomSkill = (FurNovaBlast)learnedSkills.Find(skill => skill is FurNovaBlast);
        if (BoomSkill != null) 
        {
            if (CombinationManager.Instance.ChanceCalculation(20))
            {
                GameObject currentCato = CombinationManager.Instance.lastMixCato;
                
                BoomMastery(position);       
            }
        }
         
    }

    public void ActivateMiniMix(Vector3 position)
    {
        Debug.Log("Mini mix should work");
        MiniMix BoomSkill = (MiniMix)learnedSkills.Find(skill => skill is MiniMix);
        if (BoomSkill != null)
        {
             Debug.Log("Mini mix should work");
            GameObject currentCato = CombinationManager.Instance.lastMixCato;
            if (currentCato != null)
            {
                currentCato.GetComponentInChildren<CatHead>().CatShrink();
                ParticleManager.Instance.SpawnParticle("ShrinkEffect", currentCato.transform.position);
                CombinationManager.Instance.TriggerTemporaryTimeScaleChange(1.4f);
            }
            
        }
    }

    public void ActivateRandomBurst(Vector3 position)
    {
         Debug.Log("RandomBurst mix should work");
        RandomBurst BoomSkill = (RandomBurst)learnedSkills.Find(skill => skill is RandomBurst);
        if (BoomSkill != null)
        {
         
            int randomValue = Random.Range(1, 4); // Generates a random number between 1 and 3

            switch (randomValue)
            {
                case 1:
                    VerticleBoom(position);
                    break;
                case 2:
                    HorizontalBoom(position);
                    break;
                case 3:
                    BoomMastery(position);
                    break;
                default:
                    // Optional: Handle unexpected cases (though shouldn't be needed here)
                    break;
            
            }

        }
    }

    public void VerticleBoom(Vector3 position)
    {
        //Debug.Log("Vertical Boom triggered at " + position);

                // Calculate the raycast range
                float raycastDistance = Mathf.Infinity;

                // Define a small offset to cast multiple rays for the vertical width
                float halfWidth = BlastWidthVerticle / 2;

                // Loop to cast rays across the vertical width
                for (float offset = -halfWidth; offset <= halfWidth; offset += 0.1f) // Adjust step size for finer or coarser raycasting
                {
                    // Calculate the new raycast start position
                    Vector3 raycastStartPos = new Vector3(position.x + offset, position.y, position.z);

                    // Perform raycast downwards
                    RaycastHit[] hitsDown = Physics.RaycastAll(raycastStartPos, Vector3.down, raycastDistance, LayerMask.GetMask("GatoLayer"));
                    foreach (RaycastHit hit in hitsDown)
                    {
                        Transform rootParent = hit.collider.transform.root;
                        
                        if (rootParent.CompareTag("Gato"))
                        {
                            UIManager.Instance.ShowFloatingPoints(rootParent.gameObject.transform.position, 1000f);
                            Destroy(rootParent.gameObject);
                        }
                    }

                    // Perform raycast upwards
                    RaycastHit[] hitsUp = Physics.RaycastAll(raycastStartPos, Vector3.up, raycastDistance, LayerMask.GetMask("GatoLayer"));
                    foreach (RaycastHit hit in hitsUp)
                    {
                        
                        Transform rootParent = hit.collider.transform.root;
                       
                        if (rootParent.CompareTag("Gato"))
                        {
                            UIManager.Instance.ShowFloatingPoints(rootParent.gameObject.transform.position, 1000f);
                            Destroy(rootParent.gameObject);
                        }
                                      
                    }
                }

                CombinationManager.Instance.TriggerTemporaryTimeScaleChange(1.6f);
                Vector3 newLightningPosition = position - new Vector3(0, 4f, 0);
                ParticleManager.Instance.SpawnParticle("Lightning", newLightningPosition);
    }

    public void HorizontalBoom(Vector3 position)
    {
         // Debug.Log("Horizontal Boom triggered at " + position);

                // Calculate the raycast range
                float raycastDistance = Mathf.Infinity;

                // Define a small offset to cast multiple rays for the horizontal width
                float halfDepth = horizontalBlastDepth * BlastWidthHorizontal; // Adjust for horizontal depth if needed

                // Loop to cast rays across the horizontal depth
                for (float offset = -halfDepth; offset <= halfDepth; offset += 0.1f) // Adjust step size for finer or coarser raycasting
                {
                    // Calculate the new raycast start position
                    Vector3 raycastStartPos = new Vector3(position.x, position.y - 1f, position.z + offset);

                    // Perform raycast to the left
                    RaycastHit[] hitsLeft = Physics.RaycastAll(raycastStartPos, Vector3.left, raycastDistance * 10, LayerMask.GetMask("GatoLayer"));
                    foreach (RaycastHit hit in hitsLeft)
                    {
                        Transform rootParent = hit.collider.transform.root;
                        
                        if (rootParent.CompareTag("Gato"))
                        {
                            UIManager.Instance.ShowFloatingPoints(rootParent.gameObject.transform.position, 1000f);
                            Destroy(rootParent.gameObject);
                        }
                    }

                    // Perform raycast to the right
                    RaycastHit[] hitsRight = Physics.RaycastAll(raycastStartPos, Vector3.right, raycastDistance * 10, LayerMask.GetMask("GatoLayer"));
                    foreach (RaycastHit hit in hitsRight)
                    {
                        Transform rootParent = hit.collider.transform.root;
                        
                        if (rootParent.CompareTag("Gato"))
                        {
                            UIManager.Instance.ShowFloatingPoints(rootParent.gameObject.transform.position, 1000f);
                            Destroy(rootParent.gameObject);
                        }
                    }
                }

                CombinationManager.Instance.TriggerTemporaryTimeScaleChange(1.6f);
                Vector3 newExplosionPosition = position - new Vector3(0f, 1f, 0);
                ParticleManager.Instance.SpawnParticle("Slam", newExplosionPosition);
    }

    public void BoomMastery(Vector3 position)
    {
        //Debug.Log("Kaboom now at " + position);

                // Define the blast radius
                supernovaBlastRadius = 12.0f;

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
                        float explosionForce = 2500.0f;
                        rb.AddExplosionForce(explosionForce, position, supernovaBlastRadius);
                    }
                    CatHead otherCatHead = collider.gameObject.GetComponent<CatHead>();
                    if (otherCatHead != null && otherCatHead.catType == CatType.OddCat)
                    {
                        UIManager.Instance.ShowFloatingPoints(collider.gameObject.transform.position, 1000f);
                        collider.transform.root.gameObject.SetActive(false);
                    }
                    ParticleManager.Instance.SpawnParticle("Blast", position);
                    CombinationManager.Instance.TriggerTemporaryTimeScaleChange(1.4f);
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

    public void AddSkills(string skillName)
    {
        if (skillName != null)
        {
            skillManageScript.ChooseSkill(skillName, gameObject);
        }
        
    }
}
