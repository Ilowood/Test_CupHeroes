using UnityEngine;
using Untils;

namespace Game
{
    public class DefeatState : IFSMState<StateGameplay>
    {
        private readonly GameObject _loseView;

        public DefeatState(GameObject loseView)
        {
            _loseView = loseView;
        }

        public StateGameplay State => throw new System.NotImplementedException();

        public void Enter()
        {
            _loseView.SetActive(true);
        }

        public void Exit()
        {

        }
    }
}
