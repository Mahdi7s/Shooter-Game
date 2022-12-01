using System;
using Messaging.Hub.Providers;

namespace Messaging.Listener.Contracts
{
    public interface IMessageListener<TMessage>  {
        object Listener { get; set; }
        Type MessageType { get; set; }
        string MessageId { get; set; }
        Action<MessageData<TMessage,string>> OnMessageReceived { get; set; }
    }
}
