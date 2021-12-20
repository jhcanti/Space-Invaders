using System.Collections.Generic;
using UnityEngine;

public class EventQueue : MonoBehaviour
{
    private Queue<EventData> _currentEvents;
    private Queue<EventData> _nextEvents;

    private Dictionary<EventIds, List<IEventObserver>> _observers;
        
    private void Awake()
    {
        _currentEvents = new Queue<EventData>();
        _nextEvents = new Queue<EventData>();
        _observers = new Dictionary<EventIds, List<IEventObserver>>();
    }

    public void Subscribe(EventIds eventId, IEventObserver eventObserver)
    {
        if (!_observers.TryGetValue(eventId, out var observers))
        {
            observers = new List<IEventObserver>();
        }
            
        observers.Add(eventObserver);
        _observers[eventId] = observers;
    }

    public void Unsubscribe(EventIds eventId, IEventObserver eventObserver)
    {
        if (!_observers.TryGetValue(eventId, out var observers)) return;

        _observers[eventId].Remove(eventObserver);
    }

    public void EnqueueEvent(EventData eventData)
    {
        _nextEvents.Enqueue(eventData);
    }

    private void LateUpdate()
    {
        ProcessEvents();
    }

    private void ProcessEvents()
    {
        (_currentEvents, _nextEvents) = (_nextEvents, _currentEvents);  // intercambiamos las listas

        foreach (var eventData in _currentEvents)
        {
            ProcessEvent(eventData);
        }
            
        _currentEvents.Clear();
    }

    private void ProcessEvent(EventData eventData)
    {
        if (!_observers.TryGetValue(eventData.EventId, out var observers)) return;
            
        foreach (var observer in observers)
        {
            observer.Process(eventData);
        }
    }
}
