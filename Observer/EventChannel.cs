using System.Collections.Generic;
using UnityEngine;

public abstract class EventChannel<T> : ScriptableObject
{
    private readonly HashSet<EventListener<T>> observers = new();

    public void Invoke(T value)
    {
        foreach (var VARIABLE in (observers))
        {
            VARIABLE.Raise(value);
        }
    }
    
    public void Register(EventListener<T> listener)
    {
        observers.Add(listener);
    }

    public void Deregister(EventListener<T> listener)
    {
        observers.Remove(listener);   
    }
}

// Its Okay To leave it here
public readonly struct Empty { }

[CreateAssetMenu(menuName = "Events/EventChannel")]
public class EmptyEventChannel : EventChannel<Empty> { }