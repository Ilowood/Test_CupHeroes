using UnityEngine;
using Zenject;

namespace Game
{
    public class Gameplay : MonoBehaviour
    {
        [Inject] private FSMGameplay _fSMGameplay;

        private void Start()
        {
#if UNITY_EDITOR
            EditorConfigSetup();
#endif

            _fSMGameplay.EnterIn(StateGameplay.InitState);
        }

#if UNITY_EDITOR
    [Inject] private GameplayConfig _gameplayConfig;
    [SerializeField] private EntityConfig _playerConfig;
    [SerializeField] private LevelConfig _levelConfig;

    private void EditorConfigSetup()
    {
        _gameplayConfig.Player = _playerConfig;
        _gameplayConfig.Level = _levelConfig;
    }
#endif
    }
}
