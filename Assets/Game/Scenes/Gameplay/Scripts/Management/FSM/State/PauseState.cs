using Untils;

namespace Game
{
    public class PauseState : IFSMState<StateGameplay>
    {
        public PauseState()
        {

        }

        public StateGameplay State => StateGameplay.Pause;

        public void Enter()
        {

        }

        public void Exit()
        {

        }
    }
}
