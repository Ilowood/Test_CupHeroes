using Cysharp.Threading.Tasks;
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
            await _playerController.MovementSystem.Walk(_worldController.StartPosition);
            _cameraController.SetTrackingTarget(_playerController.transform);

            var testP = _worldController.StartPosition;
            testP.x += 8;
            await _playerController.MovementSystem.Run(testP);
            //... появление хп
            _fSM.EnterIn(StateGameplay.StartGame);
        }

        public void Exit()
        {

        }
    }
}
