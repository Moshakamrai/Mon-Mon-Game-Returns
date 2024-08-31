using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameSubject gameSubject = new GameSubject(); // Observer pattern subject


    private void Start()
    {
        Application.targetFrameRate = 1000;
      // Time.timeScale = 1.6f;
    }
    private void Update()
    {
        
    }

   

    // Placeholder method to determine the position to spawn the new cat
    private Vector3 GetSpawnPosition()
    {
        // Implement your logic to determine the spawn position of the new cat
        return Vector3.zero; // Replace with actual logic
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

}
