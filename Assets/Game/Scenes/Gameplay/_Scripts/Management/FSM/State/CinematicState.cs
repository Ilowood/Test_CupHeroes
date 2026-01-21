using Cysharp.Threading.Tasks;
using UnityEngine;
using Untils;

namespace Game
{
    public class CinematicState : ISuspendFSMState<StateGameplay>
    {
        private readonly FSMGameplay _fSM;

        private readonly EntityController _playerController;
        private readonly CameraTracker _cameraTracker;
        private readonly WorldController _worldController;

        public CinematicState(FSMGameplay fSM, EntityController playerController, CameraTracker cameraTracker, WorldController worldController)
        {
            _fSM = fSM;

            _playerController = playerController;
            _cameraTracker = cameraTracker;
            _worldController = worldController;
        }

        public StateGameplay State => StateGameplay.CinematicState;

        public void Enter()
        {
            AsyncEnter().Forget();
        }

        public async UniTask AsyncEnter()
        {
            var startGamePoint = new Vector3(4f, _worldController.EnableCameraPoint.y, _worldController.EnableCameraPoint.z);

            await _playerController.MovementSystem.CreateSequence()
                .AccelerateTo(_worldController.EnableCameraPoint, m => m.Config.WalkProfile)
                .Do(() => _cameraTracker.FollowTarget(_playerController.transform))
                .Do(() => _worldController.Init(_cameraTracker))
                .DecelerateToStop(startGamePoint, m => m.Config.RunProfile)
                .RunAsync();

            _fSM.EnterIn(StateGameplay.GameLoopState);
        }

        public void Exit()
        {
            
        }

        public void Suspend()
        {
            _playerController.Pause();
        }

        public void Resume()
        {
            _playerController.Resume();
        }
    }
}
