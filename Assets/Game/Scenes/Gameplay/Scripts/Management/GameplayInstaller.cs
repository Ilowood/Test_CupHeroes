using UnityEngine;
using Zenject;

namespace Game
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private UIController _uiController;
        [SerializeField] private WorldController _worldController;
        [SerializeField] private CameraController _cameraController;
        [SerializeField] private EntityController _playerController;

        public override void InstallBindings()
        {
            Container.Bind<UIController>().FromInstance(_uiController).AsSingle();
            Container.Bind<WorldController>().FromInstance(_worldController).AsSingle();
            Container.Bind<CameraController>().FromInstance(_cameraController).AsSingle();
            Container.Bind<EntityController>().FromInstance(_playerController).AsSingle();

            Container.BindInterfacesAndSelfTo<InitState>().AsSingle();
            Container.BindInterfacesAndSelfTo<CinematicState>().AsSingle();

            Container.Bind<FSMGameplay>().AsSingle();
        }
    }
}
