using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    private List<IGameObject> gameObjects;

    void Start()
    {
        gameObjects = new List<IGameObject>();

        // Example of creating and initializing game objects
        //Fruit fruit = new GameObject("Fruit").AddComponent<Fruit>();
       // Obstacle obstacle = new GameObject("Obstacle").AddComponent<Obstacle>();

        //gameObjects.Add(fruit);
        //gameObjects.Add(obstacle);

        foreach (IGameObject obj in gameObjects)
        {
            obj.Initialize();
        }
    }

    void Update()
    {
        foreach (IGameObject obj in gameObjects)
        {
            obj.UpdateState();
        }
    }

    public void DestroyGameObject(IGameObject obj)
    {
        obj.Destroy();
        gameObjects.Remove(obj);
    }
}
