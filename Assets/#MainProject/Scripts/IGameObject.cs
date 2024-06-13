// Define the IGameObject interface
using UnityEngine;

public interface IGameObject
{
    void Initialize();  // Method to initialize the object
    void UpdateState(); // Method to update the object's state
    void Destroy();     // Method to handle the object's destruction
}

