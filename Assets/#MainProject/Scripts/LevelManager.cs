using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    // Enum to represent different levels
    public enum GameLevel
    {
        Level1 = 1,
        Level2 = 2,
        Level3 = 3,
        Level4 = 4,
        Level5 = 5
    }

    // Current level of the game
    public GameLevel currentLevel = GameLevel.Level1;

    [SerializeField] private UIManager uiManager;

    // Array of points required to advance to the next level
    [SerializeField] private float[] levelPointThresholds = { 70000f, 80000f, 100000f };

    [SerializeField] private int thesholdIndex;

    // Additional point threshold for triggering UI
    [SerializeField] private float[] uiTriggerThreshold = { 20000f, 100000f, 200000f };

    public float nextPointGoal;

    private bool uiTriggered = false;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        if (uiManager != null)
        {
            uiManager.OnPointsThresholdCrossed += OnPointsThresholdCrossed;
        }

        nextPointGoal = uiTriggerThreshold[0] * (int)currentLevel;
    }

    private void OnDestroy()
    {
        if (uiManager != null)
        {
            uiManager.OnPointsThresholdCrossed -= OnPointsThresholdCrossed;
        }
    }

    private void OnPointsThresholdCrossed(float points)
    {
        // Calculate the multiplier based on the current level
        //int levelMultiplier = (int)currentLevel;

        // Check if UI should be triggered
        if (!uiTriggered && points >= nextPointGoal)
        {
            Debug.LogError(points + "Game should be paused");
            CombinationManager.Instance.PauseGame();
            TriggerUIBetweenLevels();
            uiTriggered = true;

            // Update thesholdIndex based on the level
            thesholdIndex += 1;
           // uiTriggerThreshold[thesholdIndex] *= levelMultiplier;
            nextPointGoal = uiTriggerThreshold[thesholdIndex];
        }

        // Advance to the next level when the threshold for the current level is crossed
        if (points >= levelPointThresholds[(int)currentLevel - 1]) // Adjusted for 0-based index
        {
            // CombinationManager.Instance.PauseGame();
            // AdvanceToNextLevel();
        }
    }

    private void AdvanceToNextLevel()
    {
        if (currentLevel < GameLevel.Level5)
        {
            currentLevel++;
            AdjustDifficultyForLevel();
            Debug.Log("Advanced to " + currentLevel.ToString());
            // Reset the UI trigger for the next level
            uiTriggered = false;
        }
        else
        {
            Debug.Log("Max level reached!");
        }
    }

    private void AdjustDifficultyForLevel()
    {
        switch (currentLevel)
        {
            case GameLevel.Level1:
                // Adjust parameters for Level 1
                break;
            case GameLevel.Level2:
                // Adjust parameters for Level 2
                break;
            case GameLevel.Level3:
                // Adjust parameters for Level 3
                break;
            case GameLevel.Level4:
                // Adjust parameters for Level 4
                break;
            case GameLevel.Level5:
                // Adjust parameters for Level 5
                break;
        }
    }

    private void TriggerUIBetweenLevels()
    {
        // Trigger your UI here, e.g., show a transition screen or rewards
        Debug.Log("Triggering UI between levels");
        uiManager.ShowLevelTransitionUI(); // Example method call
    }
}
