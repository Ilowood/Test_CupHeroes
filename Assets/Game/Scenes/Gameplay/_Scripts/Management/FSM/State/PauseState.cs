using Untils;

namespace Game
{
    public class PauseState : IFSMState<StateGameplay>
    {
        private readonly PauseView _view;

        public PauseState(PauseView view)
        {
            _view = view;
            _view.Init();
        }

        public StateGameplay State => StateGameplay.PauseState;

        public void Enter()
        {
            _view.Enable();
        }

        public void Exit()
        {
            _view.Disable();
        }
    }
}
