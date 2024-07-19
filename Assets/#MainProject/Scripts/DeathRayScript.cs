using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathRayScript : MonoBehaviour
{
    [SerializeField] bool shouldDie;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Gato") && other.GetComponent<CatHead>().istouched)
        {
            shouldDie = true;
            DeathRayActivate();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Gato"))
        {
            shouldDie = false;
            StopCoroutine(DeathCounter());
        }
    }

    public void DeathRayActivate()
    {
        StartCoroutine(DeathCounter());
    }

    IEnumerator DeathCounter()
    {
        yield return new WaitForSeconds(3f);
        if (shouldDie)
        {
            Debug.Log("GAME OVER");
            CombinationManager.Instance.PauseGame();
            UIManager.Instance.GameOverPanelActive();
        }
    }
}
