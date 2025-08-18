using Game;
using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Debug.Log("Инициализация глобального контейнера");
        var config = new EntityConfig(new MoveConfig(new(1, 1, 0.6f), new(2, 2, 1.2f)), new());
        Container.Bind<EntityConfig>().FromInstance(config).AsSingle().Lazy();
    }
}   
