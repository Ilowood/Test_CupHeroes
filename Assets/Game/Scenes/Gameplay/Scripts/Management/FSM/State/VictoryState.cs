using UnityEngine;
using Untils;

namespace Game
{
    public class VictoryState : IFSMState<StateGameplay>
    {
        private readonly GameObject _winView;

        public VictoryState(GameObject loseView)
        {
            _winView = loseView;
        }

        public StateGameplay State => throw new System.NotImplementedException();

        public void Enter()
        {
            _winView.SetActive(true);
        }

        public void Exit()
        {

        }
    }
}
