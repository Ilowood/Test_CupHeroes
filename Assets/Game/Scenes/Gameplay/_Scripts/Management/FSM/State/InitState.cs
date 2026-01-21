using Untils;

namespace Game
{
    public class InitState : IFSMState<StateGameplay>
    {
        private readonly FSMGameplay _fSM;
        private readonly HUDView _hudView;
        private readonly EntityController _playerController;
        private readonly GameplayConfig _sceneConfig;

        public InitState(FSMGameplay fSMGameplay, HUDView hudView, EntityController playerController, GameplayConfig config)
        {
            _fSM = fSMGameplay;
            _playerController = playerController;
            _hudView = hudView;
            _sceneConfig = config;
        }

        public StateGameplay State => StateGameplay.InitState;

        public void Enter()
        {        
            _hudView.Init(_sceneConfig.Level.Wave.Count);    
            _playerController.Init(_sceneConfig.Player);
            
            _fSM.EnterIn(StateGameplay.CinematicState);
        }

        public void Exit()
        {

        }
    }
}
