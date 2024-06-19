using UnityEngine;

public class ScoreManager : MonoBehaviour, IGameObserver
{
    private int score;

    private void Start()
    {
        // Register this observer to the game subject
        FindObjectOfType<GameManager>().gameSubject.AddObserver(this);
    }

    public void OnCatCombined(CatType newCatType)
    {
        // Update score based on the new cat type
        score += (int)newCatType * 10;
        Debug.Log("Score updated: " + score);
    }

    private void OnDestroy()
    {
        // Unregister this observer from the game subject
        if (FindObjectOfType<GameManager>() != null)
        {
            FindObjectOfType<GameManager>().gameSubject.RemoveObserver(this);
        }
    }
}
