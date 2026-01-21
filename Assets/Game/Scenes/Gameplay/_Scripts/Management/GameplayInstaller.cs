using System;
using UnityEngine;
using Zenject;

namespace Game
{
    public class GameplayInstaller : MonoInstaller
    {
        [Header("UI")]
        [SerializeField] private HUDView _hudScreen;
        [SerializeField] private PauseView _pauseScreen;
        [SerializeField] private VictoryView _victoryScreen;
        [SerializeField] private DefeatView _defeatScreen;

        [Header("Entities Controllers")]
        [SerializeField] private WorldController _worldController;
        [SerializeField] private CameraTracker _cameraController;
        [SerializeField] private EntityController _playerController;
        
        [Header("Configs")]
        [Space, SerializeField] private LevelConfig _levelConfig;

        public override void InstallBindings()
        {            
            Container.Bind<WorldController>().FromInstance(_worldController).AsSingle();
            Container.Bind<CameraTracker>().FromInstance(_cameraController).AsSingle();
            Container.Bind<EntityController>().FromInstance(_playerController).AsSingle();

            Container.Bind<LevelConfig>().FromInstance(_levelConfig).AsSingle();

            InstallUI();
            InstallFSM();
        }

        private void InstallUI()
        {
            Container.Bind<HUDView>().FromInstance(_hudScreen).AsSingle();
            Container.Bind<PauseView>().FromInstance(_pauseScreen).AsSingle();
            Container.Bind<VictoryView>().FromInstance(_victoryScreen).AsSingle();
            Container.Bind<DefeatView>().FromInstance(_defeatScreen).AsSingle();
        }

        private void InstallFSM()
        {
            Container.BindInterfacesAndSelfTo<InitState>().AsSingle();
            Container.BindInterfacesAndSelfTo<CinematicState>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameLoopState>().AsSingle();

            Container.BindInterfacesAndSelfTo<PauseState>().AsSingle();
            Container.BindInterfacesAndSelfTo<VictoryState>().AsSingle();
            Container.BindInterfacesAndSelfTo<DefeatState>().AsSingle();

            Container.Bind<FSMGameplay>().AsSingle();
        }
    }
}
