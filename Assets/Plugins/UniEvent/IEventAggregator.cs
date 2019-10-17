using UniRx;
using System; // Required for WP8 and Store APPS

namespace uniFrame
{
    public interface IEventAggregator
    {
        IObservable<TEvent> GetEvent<TEvent>();
        void Publish<TEvent>(TEvent evt);
        bool DebugEnabled { get; set; }
    }
}