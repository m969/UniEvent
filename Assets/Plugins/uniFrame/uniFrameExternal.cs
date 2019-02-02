using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using uniFrame;


public static class uniFrameExternal {
    public static EventAggregator EventAggregator
    {
        get
        {
            if (_eventAggregator == null)
                _eventAggregator = new EventAggregator();
            return _eventAggregator;
        }
    }
    private static EventAggregator _eventAggregator;


    public static void Pubish<TEvent>(this MonoBehaviour monoBehaviour, TEvent evt)
    {
        EventAggregator.Publish<TEvent>(evt);
    }

    public static IObservable<TEvent> OnEvent<TEvent>(this MonoBehaviour monoBehaviour)
    {
        return EventAggregator.GetEvent<TEvent>();
    }
    
    public static void Pubish<TEvent>(TEvent evt)
    {
        EventAggregator.Publish<TEvent>(evt);
    }

    public static IObservable<TEvent> OnEvent<TEvent>()
    {
        return EventAggregator.GetEvent<TEvent>();
    }
}
