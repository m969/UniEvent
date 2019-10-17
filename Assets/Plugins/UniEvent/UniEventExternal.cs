using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using uniFrame;


public static class UniEventExternal {
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


    public static void Publish<TEvent>(this object o, TEvent evt)
    {
        EventAggregator.Publish<TEvent>(evt);
    }

    public static TResult Execute<TCommand, TResult>(this object o, TCommand evt)
    {
        //EventAggregator.Publish<TCommand>(evt);
        //var evtType = evt.GetType();
        //var resultField = evtType.GetField("Result");
        //var result = (TResult)resultField.GetValue(evt);
        var result = (TResult)o.Execute<TCommand>(evt);
        return result;
    }

    public static object Execute<TCommand>(this object o, TCommand evt)
    {
        EventAggregator.Publish<TCommand>(evt);
        var evtType = evt.GetType();
        var resultField = evtType.GetField("Result");
        var result = resultField.GetValue(evt);
        return result;
    }

    public static IObservable<TEvent> OnEvent<TEvent>(this object o)
    {
        return EventAggregator.GetEvent<TEvent>();
    }

    public static void Publish<TEvent>(TEvent evt)
    {
        EventAggregator.Publish<TEvent>(evt);
    }

    public static IObservable<TEvent> OnEvent<TEvent>()
    {
        return EventAggregator.GetEvent<TEvent>();
    }
}
