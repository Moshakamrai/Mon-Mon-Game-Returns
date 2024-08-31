using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public Sprite[] blobSprites;
    public Image blobUI;
    public Slider slowMotionBar;

    private Coroutine slowMotionCoroutine;
    private float smoothSpeed = 0.3f; // Speed at which the slider value changes

    [SerializeField] private DynamicTextData floatingFont;

    [SerializeField] private TextMeshProUGUI pointText;

    [SerializeField] private float totalPoints;

    [SerializeField] private GameObject gameOverPanel;

    [SerializeField] private GameObject[] allPowerUpChooser;

    private List<int> chosenIndices = new List<int>();

    private List<int> shownPowerUps = new List<int>();

    [SerializeField] private GameObject powerUpPanel; // UI panel for level transitions

    // Define a delegate and event for the point threshold crossing
    public delegate void PointsThresholdCrossedHandler(float points);
    public event PointsThresholdCrossedHandler OnPointsThresholdCrossed;

    // The threshold value for triggering the event
    [SerializeField] private float threshold = 1000f; // Example threshold

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
        // Initialize the slow motion bar to full value
        slowMotionBar.maxValue = 1;
        slowMotionBar.value = 1;
        CameraShake.Instance.StopShake();
        //ShowLevelTransitionUI();
    }

    public void SetMaxSlowMotionBar(float value)
    {
        slowMotionBar.maxValue = value;
        slowMotionBar.value = value;
    }

    public void StartDecreaseSlowMotionBar()
    {
        if (slowMotionCoroutine != null)
        {
            StopCoroutine(slowMotionCoroutine);
        }
        slowMotionCoroutine = StartCoroutine(DecreaseSlowMotionBar());
    }

    public void StartIncreaseSlowMotionBar()
    {
        if (slowMotionCoroutine != null)
        {
            StopCoroutine(slowMotionCoroutine);
        }
        slowMotionCoroutine = StartCoroutine(IncreaseSlowMotionBar());
    }

    private IEnumerator DecreaseSlowMotionBar()
    {
        while (slowMotionBar.value > 0)
        {
            slowMotionBar.value -= smoothSpeed * Time.unscaledDeltaTime;
            yield return null;
        }

        slowMotionBar.value = 0;
    }

    private IEnumerator IncreaseSlowMotionBar()
    {
        yield return new WaitForSeconds(1f);
        while (slowMotionBar.value < 1)
        {
            slowMotionBar.value += smoothSpeed * Time.deltaTime;
            yield return null;
        }

        slowMotionBar.value = 1;
    }

    public void ShowFloatingPoints(Vector3 position, float mixPoints)
    {
        float x = UnityEngine.Random.Range(0.1f, 2); // Explicitly use UnityEngine.Random
        float y = UnityEngine.Random.Range(2f, 4);  // Explicitly use UnityEngine.Random
        AddPoints(mixPoints);
        position += new Vector3(x, y, 0);
        DynamicTextManager.CreateText(position, mixPoints.ToString(), floatingFont);
    }

    public void ShowFloatingPointsSmall(Vector3 position, float mixPoints)
    {
        float x = UnityEngine.Random.Range(0.1f, 2); // Explicitly use UnityEngine.Random
        float y = UnityEngine.Random.Range(2f, 4);  // Explicitly use UnityEngine.Random
        AddPoints(mixPoints);
        position += new Vector3(x, y, 0);
        DynamicTextManager.CreateTextNew(position, mixPoints.ToString(), floatingFont);
    }

    public void AddPoints(float pointsScored)
    {
        totalPoints += pointsScored;
        pointText.text = totalPoints.ToString();

        // Check if the total points have crossed the threshold
        if (totalPoints >= LevelManager.Instance.nextPointGoal)
        {
            // Trigger the event
            OnPointsThresholdCrossed?.Invoke(totalPoints);
            //threshold *= 3;
        }
    }

    public void GameOverPanelActive()
    {
        gameOverPanel.SetActive(true);
    }

    // Optional: Method to update the threshold
    public void SetThreshold(float newThreshold)
    {
        threshold = newThreshold;
    }

    // Method to show the level transition UI
    public void ShowLevelTransitionUI()
    {
        if (powerUpPanel != null)
        {
            RandomPowerUpPool();
            
            powerUpPanel.SetActive(true);
            // Optionally add animation or delay here
        }
    }

    // Method to hide the level transition UI (e.g., after the player clicks to continue)
    public void HideLevelTransitionUI()
    {
        if (powerUpPanel != null)
        {
            CombinationManager.Instance.UnpauseGame();
            powerUpPanel.SetActive(false);
            CameraShake.Instance.StopShake();
        }
    }


    public void RandomPowerUpPool()
    {
        // Deactivate all power-ups to reset the state
        foreach (GameObject powerUp in allPowerUpChooser)
        {
            powerUp.SetActive(false);
        }

        // List to hold the indices of available power-ups
        List<int> availableIndices = new List<int>();
        for (int i = 0; i < allPowerUpChooser.Length; i++)
        {
            if (!shownPowerUps.Contains(i)) // Only include indices that have not been shown
            {
                availableIndices.Add(i);
            }
        }

        // Ensure there are at least 3 available indices
        if (availableIndices.Count < 3)
        {
            Debug.LogWarning("Not enough power-ups available to choose from.");
            return;
        }

        // Randomly select 3 unique indices
        List<int> selectedIndices = new List<int>();
        while (selectedIndices.Count < 3)
        {
            int randomIndex = Random.Range(0, availableIndices.Count);
            int chosenIndex = availableIndices[randomIndex];

            if (!selectedIndices.Contains(chosenIndex))
            {
                selectedIndices.Add(chosenIndex);
            }
        }

        // Activate the selected power-ups
        foreach (int index in selectedIndices)
        {
            allPowerUpChooser[index].SetActive(true);
        }

        // Track the shown power-ups
        foreach (int index in selectedIndices)
        {
            TrackChosenPowerUp(index);
        }

        // Add the selected indices to the list of shown power-ups
        shownPowerUps.AddRange(selectedIndices);
    }

    public void TrackChosenPowerUp(int index)
    {
        // Add the chosen index to the list to ensure it's not picked again in this session
        if (!chosenIndices.Contains(index))
        {
            chosenIndices.Add(index);
        }
    }

    public void ResetShownPowerUps()
    {
        // Clear the list of shown power-ups to allow them to be activated again
        shownPowerUps.Clear();
    }
}

