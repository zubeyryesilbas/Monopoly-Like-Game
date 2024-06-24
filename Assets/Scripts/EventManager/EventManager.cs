using System;
using System.Collections.Generic;

public class EventManager
{
    private static EventManager _instance;

    private Dictionary<string, List<Action<object>>> _eventListeners = new Dictionary<string, List<Action<object>>>();

    public static EventManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new EventManager();
            }
            return _instance;
        }
    }

    public void AddEventListener(string eventName, Action<object> action)
    {
        if (!_eventListeners.ContainsKey(eventName))
        {
            _eventListeners[eventName] = new List<Action<object>>();
        }

        _eventListeners[eventName].Add(action);
    }

    public void RemoveEventListener(string eventName, Action<object> action)
    {
        if (_eventListeners.ContainsKey(eventName))
        {
            _eventListeners[eventName].Remove(action);
        }
    }

    public void TriggerEvent(string eventName, object eventData = null)
    {
        if (_eventListeners.ContainsKey(eventName))
        {
            foreach (var action in _eventListeners[eventName])
            {
                action.Invoke(eventData);
            }
        }
    }
}