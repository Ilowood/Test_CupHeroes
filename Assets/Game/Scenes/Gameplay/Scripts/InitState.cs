using UnityEngine;
using Untils;

namespace Game
{
    public class InitState : IFSMState<StateGameplay>
    {
        private readonly FSMGameplay _fSM;
        private readonly RectTransform _saveScreen;

        public InitState(FSMGameplay fSM, RectTransform saveScreen)
        {
            _fSM = fSM;
            _saveScreen = saveScreen;
        }

        public void Enter()
        {
            UI.SaveArea(_saveScreen);
            _fSM.EnterIn(StateGameplay.Intro);
        }

        public void Exit()
        {

        }
    }
}
