using System.Collections.Generic;

public class GameSubject
{
    private List<IGameObserver> observers = new List<IGameObserver>();

    public void AddObserver(IGameObserver observer)
    {
        observers.Add(observer);
    }

    public void RemoveObserver(IGameObserver observer)
    {
        observers.Remove(observer);
    }

    public void NotifyCatCombined(CatType newCatType)
    {
        foreach (var observer in observers)
        {
            observer.OnCatCombined(newCatType);
        }
    }
}

public interface IGameObserver
{
    void OnCatCombined(CatType newCatType);
}
