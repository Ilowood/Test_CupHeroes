using UnityEngine;
using Untils;

namespace Game
{
    public class CinematicState : IFSMState<StateGameplay>
    {
        private readonly FSMGameplay _fSM;

        private readonly EntityController _playerController;
        private readonly CameraController _cameraController;
        private readonly Transform _startPoint;

        public CinematicState(FSMGameplay fSM, EntityController playerController, CameraController cameraController, Transform startPoint)
        {
            _fSM = fSM;

            _playerController = playerController;
            _cameraController = cameraController;
            _startPoint = startPoint;
        }

        public async void Enter()
        {
            var pos = new Vector2(_startPoint.position.x, _startPoint.position.y);
            await _playerController.SyncMoveTo(pos, new(0, 1), new(0, 0.5f));
            
            _cameraController.SetTrackingTarget(_playerController.transform);
            //... появление хп
            _fSM.EnterIn(StateGameplay.StartGame);
        }

        public void Exit()
        {

        }
    }
}
