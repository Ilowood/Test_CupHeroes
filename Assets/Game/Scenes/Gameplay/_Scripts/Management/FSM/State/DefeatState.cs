using Untils;

namespace Game
{
    public class DefeatState : IFSMState<StateGameplay>
    {
        private readonly DefeatView _view;

        public DefeatState(DefeatView view)
        {
            _view = view;
            _view.Init();
        }

        public StateGameplay State => StateGameplay.DefeatState;

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
