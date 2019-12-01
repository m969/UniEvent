using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniEvent;

namespace UniEvent
{

    public interface ICommand
    {
        object Result { get; set; }
    }

    public interface ICommand<TResult>
    {
        TResult Result { get; set; }
    }

    public static class UniEventExternal
    {
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

        public static TResult Execute<TCommand, TResult>(this object o, TCommand evt) where TCommand : ICommand<TResult>
        {
            EventAggregator.Publish(evt);
            return evt.Result;
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

}