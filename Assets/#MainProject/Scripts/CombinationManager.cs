using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class CombinationManager : MonoBehaviour
{
    public static CombinationManager Instance { get; private set; }

    public GameObject[] catPrefabs; // Array of cat prefabs in hierarchical order
    public int catCount;

    public ObjectSpawnerController objectSpawnerScript;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optionally keep the instance alive across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }

    public GameObject CombineCats(GameObject cat1, GameObject cat2)
    {
        catCount++;
        CatHead head1 = cat1.GetComponent<CatHead>();
        CatHead head2 = cat2.GetComponent<CatHead>();

        if (head1 != null && head2 != null && head1.catType == head2.catType && head1.catType < CatType.OddCat && catCount % 2 == 0)
        {
            CatType newType = head1.catType + 1;
            GameObject newCat = Instantiate(catPrefabs[(int)newType], cat1.transform.position, Quaternion.Euler(0, 180, 0));

            // Trigger the combo event

            SkillEvents.Instance.ComboMade(cat1.transform.position);
            return newCat;
        }
        //if (catCount % 2 == 0)
        //{
        //    SkillEvents.Instance.ComboMade(cat1.transform.position);
        //}
        return null;
    }

    // Function to change time scale to 0.5 for 2 seconds and then back to 1.4
    public void TriggerTemporaryTimeScaleChange(float duration)
    {
        StartCoroutine(TemporaryTimeScaleChange(0.3f, duration, 1.5f));
    }
    
    private IEnumerator TemporaryTimeScaleChange(float targetTimeScale, float duration, float finalTimeScale)
    {
        Time.timeScale = targetTimeScale;
        Time.fixedDeltaTime = Time.timeScale * 0.02f; // Adjust fixedDeltaTime according to time scale
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = finalTimeScale;
        Time.fixedDeltaTime = Time.timeScale * 0.02f; // Adjust fixedDeltaTime according to time scale
    }

    public void customTimeScale(float timeScaleAmmount)
    {
        Debug.Log("Time should slow");
        Time.timeScale = timeScaleAmmount;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }

    public void ControlGame(bool gameBool)
    {
        objectSpawnerScript.GameController(gameBool);
    }

    public void PauseGame()
    {
        objectSpawnerScript.StopSpawn();
    }
}

public enum CatType
{
    Cat1,
    Cat2,
    Cat3,
    Cat4,
    Cat5,
    Cat6,
    Cat7,
    Cat8,
    Cat9,
    OddCat
}
