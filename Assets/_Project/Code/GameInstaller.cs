using Zenject;

public class GameInstaller : MonoInstaller
{
    override public void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<NetworkMessagesService>().AsSingle();
    }
}
