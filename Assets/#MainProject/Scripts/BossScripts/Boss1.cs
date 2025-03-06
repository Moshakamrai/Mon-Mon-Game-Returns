using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : MonoBehaviour
{
    public int bossMix;

    private int score;
    public int bossHealth;

    public int bossStatMultiplier;

    public int maxBossHealth = 500; // Set your boss's starting max health
    // Start is called before the first frame update
    

    void Start()
    {
        CombinationManager.Instance.bossBuff += HandleComboMade;
        CombinationManager.Instance.onPointDamage += GetDamage2;
        //bossMix -= bossStatMultiplier;
        //bossHealth *= bossStatMultiplier;
        if (UIManager.Instance != null)
        {
            UIManager.Instance.bossHealthSlider.gameObject.SetActive(true);
            UIManager.Instance.bossHealthText.gameObject.SetActive(true);
        }
        if (UIManager.Instance != null)
        {
            UIManager.Instance.bossHealthSlider.maxValue = 1; // Slider always goes from 0 to 1
            UpdateBossUI(); // Set initial health values
        }
    }

    private void OnDestroy()
    {
        if (UIManager.Instance != null)
        {
            //UIManager.Instance.onPointDamage -= OnPointsDamage;
        }
    }

    public void OnPointsDamage()
    {

    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        CombinationManager.Instance.bossBuff -= HandleComboMade;
        //SkillEvents.Instance.OnComboMade -= HandleComboMade;
    }

    private void HandleComboMade()
    {
        Debug.Log("Boss is enlarging");
        bossHealth += 20;

        Transform rootParent = transform.root; // Gets the highest parent in the hierarchy
        rootParent.localScale += new Vector3(0, 0.2f, 0.2f); 
        ParticleManager.Instance.SpawnParticle("BossBuff", gameObject.transform.position);
        UpdateBossUI(); // Update health UI
    }

    public void GetDamage(int damage)
    {
        bossHealth -= damage;

        bossHealth = Mathf.Clamp(bossHealth, 0, maxBossHealth); // Prevent negative health

        transform.root.localScale -= new Vector3(0, 0.3f, 0.3f); 
        ParticleManager.Instance.SpawnParticle("BossDamage", gameObject.transform.position);
        UpdateBossUI(); // Update UI after changing health
    }


    private void UpdateBossUI()
    {
        if (UIManager.Instance != null)
        {
            // Normalize health: bossHealth / maxHealth (Ensure maxHealth is set)
            UIManager.Instance.bossHealthSlider.value = (float)bossHealth / maxBossHealth;
            UIManager.Instance.bossHealthText.text = bossHealth.ToString(); // Show exact health number
        }
        if (bossHealth <= 0)
        {
            Destroy(gameObject);
            
        }
    }

    public void GetDamage2()
    {
        bossHealth -= 5;

        bossHealth = Mathf.Clamp(bossHealth, 0, maxBossHealth); // Prevent negative health

        transform.root.localScale -= new Vector3(0, 0.05f, 0.05f); 

        UpdateBossUI(); // Update UI after changing health
    }

    
}
