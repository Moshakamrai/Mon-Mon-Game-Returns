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
    }

    //public void SetNextBlobUI(int blobIndex)
    //{
    //    blobUI.sprite = blobSprites[blobIndex];
    //}

    //public void UpdateSlowMotionBar(float value)
    //{
    //    slowMotionBar.value = value;
    //}

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
        float x = Random.Range(0.1f, 2);
        float y = Random.Range(2f, 4);
        AddPoints(mixPoints);
        position += new Vector3(x, y, 0);
        DynamicTextManager.CreateText(position, mixPoints.ToString(), floatingFont);
    }

    public void ShowFloatingPointsSmall(Vector3 position, float mixPoints)
    {
        float x = Random.Range(0.1f, 2);
        float y = Random.Range(2f, 4);
        AddPoints(mixPoints);
        position += new Vector3(x, y, 0);
        DynamicTextManager.CreateTextNew(position, mixPoints.ToString(), floatingFont);
    }

    public void AddPoints(float pointsScored)
    {
        totalPoints += pointsScored;
        pointText.text = totalPoints.ToString();
    }

    public void GameOverPanelActive()
    {
        gameOverPanel.SetActive(true);
    }
}
