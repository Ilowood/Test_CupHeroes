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

        public async void Enter()
        {
            await _playerController.MovementSystem.SyncWalk(_worldController.StartPosition);
            _cameraController.SetTrackingTarget(_playerController.transform);
            //... появление хп
            _fSM.EnterIn(StateGameplay.StartGame);
        }

        public void Exit()
        {

        }
    }
}
