using Mirror;
using UnityEngine;

public class SubscriptionManager : NetworkBehaviour
{
    public override void OnStartClient()
    {
        base.OnStartClient();

        NetworkClient.RegisterHandler<HelloMessage>(OnHelloMessageReceived);

        NetworkClient.Send<SubscriptionRequestMessage>(new SubscriptionRequestMessage
        {
            MessageTypeName = nameof(HelloMessage)
        });
    }

    private void OnHelloMessageReceived(HelloMessage message)
    {
        Debug.Log($"{message.Content}");
    }
}
