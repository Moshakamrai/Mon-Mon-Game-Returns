using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpPanelScript : MonoBehaviour
{
    
    
    private void Start()
    {
        //ombinationManager.Instance.customTimeScale(0.1f);
        CombinationManager.Instance.ControlGame(false);
    }

    private void OnDisable()
    {
        CombinationManager.Instance.ControlGame(true);
        //CombinationManager.Instance.customTimeScale(1.6f);
    }
}
