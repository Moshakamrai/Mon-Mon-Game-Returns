using System.Collections;
using System.Collections.Generic;
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

    public void SetNextBlobUI(int blobIndex)
    {
        blobUI.sprite = blobSprites[blobIndex];
    }

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
}
