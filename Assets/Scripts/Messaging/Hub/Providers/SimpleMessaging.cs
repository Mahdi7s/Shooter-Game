using Messaging.Listener.Contracts;
using Messaging.Listener.Providers;
using System;
using System.Collections.Generic;

namespace Messaging.Hub.Providers
{
    public class SimpleMessaging
    {
        private readonly List<object> _listeners;
        private static SimpleMessaging _instance;
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        public static SimpleMessaging Instance => _instance ?? (_instance = new SimpleMessaging());

        private SimpleMessaging()
        {
            if (_listeners == null)
                _listeners = new List<object>();
        }

        public void SendMessage<TMessage>(MessageData<TMessage, string> message)
        {
            var tempListeners = new List<object>(_listeners);
            foreach (var listener in tempListeners)
            {
                var castedListener = listener as IMessageListener<TMessage>;
                if (castedListener != null && castedListener.ToString().ToLower() != "null")
                {
                    if (castedListener.Listener != null && castedListener.Listener.ToString().ToLower() != "null")
                    {
                        if (castedListener.MessageType == typeof(TMessage) && string.IsNullOrEmpty(castedListener.MessageId))
                        {
                            castedListener.OnMessageReceived(message);
                        }
                    }
                }
            }
        }
        public void SendMessage<TMessage>(MessageData<TMessage, string> message, string messageId)
        {
            var tempListeners = new List<object>(_listeners);
            foreach (var listener in tempListeners)
            {
                var castedListener = listener as IMessageListener<TMessage>;
                if (castedListener != null && castedListener.ToString().ToLower() != "null")
                {
                    if (castedListener.Listener != null && castedListener.Listener.ToString().ToLower() != "null")
                    {
                        if (castedListener.MessageType == typeof(TMessage) && castedListener.MessageId == messageId)
                        {
                            castedListener.OnMessageReceived(message);
                        }
                    }
                }
            }
        }

        public void Register<TMessage>(object owner, Action<MessageData<TMessage, string>> messageReceived)
        {
            _listeners.Add(new SimpleMessageListener<TMessage>() { Listener = owner, MessageType = typeof(TMessage), OnMessageReceived = messageReceived });
        }
        public void Register<TMessage>(object owner, Action<MessageData<TMessage, string>> messageReceived, string messageId)
        {
            _listeners.Add(new SimpleMessageListener<TMessage>() { Listener = owner, MessageType = typeof(TMessage), OnMessageReceived = messageReceived, MessageId = messageId });
        }

        public void UnRegister<TMessage>(object owner)
        {
            var listenersToRemove = new List<IMessageListener<TMessage>>();
            foreach (var listener in _listeners)
            {
                var casteListener = listener as IMessageListener<TMessage>;
                if (casteListener != null)
                {
                    if (casteListener.MessageType == typeof(TMessage) && casteListener.Listener == owner)
                    {
                        listenersToRemove.Add(casteListener);
                    }
                }
            }
            listenersToRemove.ForEach(l => { _listeners.Remove(l); });
        }
    }

    public class MessageData<TData, TCommand>
    {
        public MessageData() { }
        public MessageData(TData message, TCommand command)
        {
            Command = command;
            Message = message;
        }
        public TCommand Command { get; set; }
        public TData Message { get; set; }
    }
}
