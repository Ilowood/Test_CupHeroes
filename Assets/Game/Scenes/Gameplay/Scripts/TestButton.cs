using UnityEngine;
using Zenject;

namespace Game
{
    public class TestButton : MonoBehaviour
    {
        [Inject] private FSMGameplay _fSMGameplay;

        public void Pause()
        {
            _fSMGameplay.SuspendAndEnterIn(StateGameplay.Pause);
        }

        public void Resume()
        {
            _fSMGameplay.ExitAndResume();
        }
    }
}
