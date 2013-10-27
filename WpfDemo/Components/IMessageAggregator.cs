using System;

namespace WpfDemo.Components
{
    public interface IMessageAggregator
    {
        /// <summary>
        /// Publish invokes defined action for every subscribed consumer for given message type.
        /// </summary>
        void Publish<T>(T message);

        /// <summary>
        /// Subscribe action to execute for certain message type.
        /// </summary>
        void Subscribe<T>(Action<T> action);
    }
}