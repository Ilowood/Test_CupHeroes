using Game;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<GameplayConfig>().AsSingle().Lazy();
    }
}   
