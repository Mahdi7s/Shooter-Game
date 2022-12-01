namespace Messaging.Hub.Contracts
{
    public interface IMessageHub
    {

        void AddToHub(object messageId, object message);
        object GetFromHub(object messageId);
        void ClearHub();
    }
}
