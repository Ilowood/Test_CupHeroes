using UnityEngine;
using Untils;

namespace Game
{
    public class WinState : IFSMState<StateGameplay>
    {
        private readonly GameObject _winView;

        public WinState(GameObject loseView)
        {
            _winView = loseView;
        }

        public void Enter()
        {
            _winView.SetActive(true);
        }

        public void Exit()
        {

        }
    }
}
