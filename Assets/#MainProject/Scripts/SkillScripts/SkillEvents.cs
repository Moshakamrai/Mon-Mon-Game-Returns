using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SkillEvents : MonoBehaviour
{
    public static SkillEvents Instance { get; private set; }

    public float comboTimeWindow = 6f; // Time window to stack combos
    public int comboCounter = 0;      // Tracks the current combo count
    private ComboState currentState = ComboState.Idle;
    private Coroutine comboCoroutine;

    // Call this function to trigger the combo
    

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        comboCounter = 1;
        comboTimeWindow = 5.75f; 
    }

    public event Action<Vector3> OnComboMade;

    public void ComboMade(Vector3 position)
    {
       // Debug.Log("ComboMade");
        TriggerCombo();
        UIManager.Instance.ShowFloatingPoints(position, comboCounter * 1000f);
        OnComboMade?.Invoke(position);
        
    }

    public void TriggerCombo()
    {
        if (currentState == ComboState.Idle)
        {
            // Start the combo
            comboCounter = 1;
            currentState = ComboState.ComboActive;
            Debug.Log("Combo: " + comboCounter);

            // Start the timing coroutine
            comboCoroutine = StartCoroutine(ComboTimingCoroutine());
        }
        else if (currentState == ComboState.ComboActive)
        {
            // Continue the combo
            comboCounter++;
            Debug.Log("Combo: " + comboCounter);

            // Restart the timing coroutine
            if (comboCoroutine != null)
            {
                StopCoroutine(comboCoroutine);
            }
            comboCoroutine = StartCoroutine(ComboTimingCoroutine());
        }
    }

    // Coroutine to handle the combo timing
    private IEnumerator ComboTimingCoroutine()
    {
        yield return new WaitForSeconds(comboTimeWindow);

        // Combo window has expired, reset the combo counter
        comboCounter = 0;
        currentState = ComboState.ComboReset;
        Debug.Log("Combo reset");

        // Transition back to idle state
        currentState = ComboState.Idle;
    }

}

public enum ComboState
{
    Idle,
    ComboActive,
    ComboReset
}
