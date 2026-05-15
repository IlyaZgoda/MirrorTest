using Mirror;
using System.Collections.Generic;


public interface INetworkMessagesService
{
    void Subscribe(string messageTypeName, NetworkConnectionToClient subscriber);
    void Send<TMessage>(TMessage message, NetworkConnectionToClient subscriber) 
        where TMessage : struct, NetworkMessage;
    void Unsubscribe(NetworkConnectionToClient subscriber);
}
public class NetworkMessagesService : INetworkMessagesService
{
    private readonly Dictionary<string, HashSet<NetworkConnectionToClient>> _subscribers = new();

    public void Subscribe(string messageTypeName, NetworkConnectionToClient subscriber)
    {
        if (!_subscribers.TryGetValue(messageTypeName, out var subscribers))
        {
            subscribers = new HashSet<NetworkConnectionToClient>();
            _subscribers[messageTypeName] = subscribers;
        }
        subscribers.Add(subscriber);
    }

    public void Unsubscribe(NetworkConnectionToClient subscriber)
    {
        foreach (var subscribers in _subscribers.Values)
        {
            if (subscribers.Contains(subscriber))
            {
                subscribers.Remove(subscriber);
            }
        }
    }

    public void Send<TMessage>(TMessage message, NetworkConnectionToClient subscriber) 
        where TMessage : struct, NetworkMessage
    {
        string messageTypeName = typeof(TMessage).Name;

        if (_subscribers.TryGetValue(messageTypeName, out var subscribers) && subscribers.Contains(subscriber))
        {
            subscriber.Send(message);
        }
    }
}
