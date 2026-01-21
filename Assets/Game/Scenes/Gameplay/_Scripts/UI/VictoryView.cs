using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game
{
    public class VictoryView : BaseView
    {
        [Inject] private FSMGameplay _fSMGameplay;

        [SerializeField] private Button _mainMenu;

        public void Init()
        {
            _mainMenu.onClick.AddListener(() => _fSMGameplay.Exit(GameScenes.Menu));
        }
    }
}
