using Untils;

namespace Game
{
    public class InitState : IFSMState<StateGameplay>
    {
        private readonly FSMGameplay _fSM;
        private readonly UIController _uiController;
        private readonly EntityController _playerController;
        private readonly EntityConfig _entityConfig;

        public InitState(FSMGameplay fSMGameplay, UIController uiController, EntityController playerController, EntityConfig entityConfig)
        {
            _fSM = fSMGameplay;
            _uiController = uiController;
            _playerController = playerController;
            _entityConfig = entityConfig;
        }

        public StateGameplay State => StateGameplay.Init;

        public void Enter()
        {
            UI.SaveArea(_uiController.SaveArea);

            //тут создать персонажа и закинуть конфиг
            _playerController.Init(_entityConfig);

            _fSM.EnterIn(StateGameplay.Cinematic);
        }

        public void Exit()
        {

        }
    }
}
