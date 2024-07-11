using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SkillEvents : MonoBehaviour
{
    public static SkillEvents Instance { get; private set; }

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
    }

    public event Action<Vector3> OnComboMade;

    public void ComboMade(Vector3 position)
    {
        Debug.Log("ComboMade");
        OnComboMade?.Invoke(position);
    }
}