using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : MonoBehaviour
{
    public int bossMix;

    private int score;
    public int bossHealth;

    public int bossStatMultiplier;
    // Start is called before the first frame update
    

    void Start()
    {
         bossMix -= bossStatMultiplier;
        bossHealth *= bossStatMultiplier;
        if (UIManager.Instance != null)
        {
            //UIManager.Instance.onPointDamage += OnPointsDamage;
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
        SkillEvents.Instance.OnComboMade += HandleComboMade;
    }

    private void OnDisable()
    {
        SkillEvents.Instance.OnComboMade -= HandleComboMade;
    }

    private void HandleComboMade(Vector3 position)
    {

        if (CombinationManager.Instance.catCount % bossMix == 0)
        {
            bossHealth += 5000;
        }
    }

    
}
