using System;
using Messaging.Hub.Providers;
using Messaging.Listener.Contracts;

namespace Messaging.Listener.Providers
{
    public class SimpleMessageListener<TMessage> : IMessageListener<TMessage>
    {
        public object Listener { get; set; }
        public Type MessageType { get; set; }
        public string MessageId { get; set; }
        public Action<MessageData<TMessage, string>> OnMessageReceived { get; set; }
    }
}