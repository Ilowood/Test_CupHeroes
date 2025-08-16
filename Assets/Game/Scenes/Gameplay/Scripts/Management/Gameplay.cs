using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Game
{
    public class Gameplay : MonoBehaviour
    {
        [Inject] private FSMGameplay _fSMGameplay;

        private void Awake()
        {
            _fSMGameplay.EnterIn(StateGameplay.Init);
        }

        public void MainSceneLoad()
        {
            SceneManager.LoadScene(0);
        }
    }
}
