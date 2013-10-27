using System;
using System.Collections.Generic;
using System.Linq;

namespace WpfDemo.Components
{
    public class MessageAggregator : IMessageAggregator
    {
        private readonly List<MessageAndTypePair> _messageList;

        public MessageAggregator()
        {
            _messageList = new List<MessageAndTypePair>();
        }

        private class MessageAndTypePair
        {
            public Type Type { get; private set; }
            public object Action { get; private set; }

            public MessageAndTypePair(Type type, object action)
            {
                Type = type;
                Action = action;
            }
        }

        public void Publish<T>(T message)
        {
            _messageList
                .Where(o => o.Type == typeof (T))
                .ToList()
                .ForEach(o => ((Action<T>) o.Action)(message));
        }

        public void Subscribe<T>(Action<T> action)
        {
            _messageList.Add(new MessageAndTypePair(typeof(T), action));
        }
    }
}
