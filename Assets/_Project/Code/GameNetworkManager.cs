using Mirror;
using Zenject;

public class GameNetworkManager : NetworkManager
{
    private INetworkMessagesService _networkMessagesService;

    [Inject]
    public void Construct(INetworkMessagesService networkMessagesService) =>
        _networkMessagesService = networkMessagesService;

    public override void OnStartServer()
    {
        base.OnStartServer();

        NetworkServer.RegisterHandler<SubscriptionRequestMessage>(OnSubscriptionRequested);
    }

    private void OnSubscriptionRequested(NetworkConnectionToClient client, SubscriptionRequestMessage message)
    {
        _networkMessagesService.Subscribe(message.MessageTypeName, client);
        _networkMessagesService.Send(new HelloMessage("Hello Client!"), client);
    }


    override public void OnServerDisconnect(NetworkConnectionToClient client)
    {
        base.OnServerDisconnect(client);

        _networkMessagesService.Unsubscribe(client);
    }
}
