using UnityEngine;
using Untils;

namespace Game
{
    public class LoseState : IFSMState<StateGameplay>
    {
        private readonly GameObject _loseView;

        public LoseState(GameObject loseView)
        {
            _loseView = loseView;
        }

        public void Enter()
        {
            _loseView.SetActive(true);
        }

        public void Exit()
        {

        }
    }
}
