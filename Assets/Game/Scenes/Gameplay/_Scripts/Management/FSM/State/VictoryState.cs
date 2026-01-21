using Untils;

namespace Game
{
    public class VictoryState : IFSMState<StateGameplay>
    {
        private readonly VictoryView _view;

        public VictoryState(VictoryView view)
        {
            _view = view;
            _view.Init();
        }

        public StateGameplay State => StateGameplay.VictoryState;

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
