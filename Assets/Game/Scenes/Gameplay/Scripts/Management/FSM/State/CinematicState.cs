using Cysharp.Threading.Tasks;
using UnityEngine;
using Untils;

namespace Game
{
    public class CinematicState : IFSMState<StateGameplay>
    {
        private readonly FSMGameplay _fSM;

        private readonly EntityController _playerController;
        private readonly CameraController _cameraController;
        private readonly WorldController _worldController;

        public CinematicState(FSMGameplay fSM, EntityController playerController, CameraController cameraController, WorldController worldController)
        {
            _fSM = fSM;

            _playerController = playerController;
            _cameraController = cameraController;
            _worldController = worldController;
        }

        public StateGameplay State => StateGameplay.Cinematic;

        public void Enter()
        {
            AsyncEnter().Forget();
        }

        public async UniTask AsyncEnter()
        {
            var testP = new UnityEngine.Vector3(12, _worldController.StartPosition.y, _worldController.StartPosition.z);

            await _playerController.MovementSystem.CreateSequence()
                .AccelerateTo(_worldController.StartPosition, m => m.Config.WalkProfile)
                .Do(() => Debug.Log("Подключение систем"))
                .Do(() => _cameraController.SetTrackingTarget(_playerController.transform))
                .DecelerateToStop(testP, m => m.Config.RunProfile)
                .RunAsync();

            //... появление хп
            _fSM.EnterIn(StateGameplay.StartGame);
        }

        public void Exit()
        {

        }
    }
}
