using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public abstract class EventListener<T> : MonoBehaviour
{
    [SerializeField] EventChannel<T> eventChannel;
    [SerializeField] private UnityEvent<T> unityEvent;

    protected void Awake()
    {
        eventChannel.Register(this);
    }

    protected void OnDestroy()
    {
        eventChannel.Deregister(this);
    }

    public void Raise(T value)
    {
        unityEvent?.Invoke(value);
    }
}

public class EventListener : EventListener<Empty> { }