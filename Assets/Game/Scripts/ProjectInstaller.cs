using Game;
using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Debug.Log("Инициализация глобального контейнера");
        Container.Bind<EntityConfig>().AsSingle().Lazy();
    }
}   
