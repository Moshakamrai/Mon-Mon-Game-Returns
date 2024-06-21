using UnityEngine;

public class CombinationManager : MonoBehaviour
{
    public static CombinationManager Instance { get; private set; }

    public GameObject[] catPrefabs; // Array of cat prefabs in hierarchical order

    public int catCount;

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

        if (head1 != null && head2 != null && head1.catType == head2.catType && head1.catType < CatType.Cat5 && catCount%2 == 1)
        {
            CatType newType = head1.catType + 1;
            GameObject newCat = Instantiate(catPrefabs[(int)newType]);
            return newCat;
        }
        return null;
    }
}



public enum CatType
{
    Cat1,
    Cat2,
    Cat3,
    Cat4,
    Cat5,
    //Cat6,
    //Cat7,
    //Cat8,
    //Cat9,
    //Cat10
}
