using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Untils;
using Zenject;

namespace Game
{
    public class HUDView : BaseView
    {
        [Inject] private FSMGameplay _fSMGameplay;

        [SerializeField] private RectTransform _saveArea;
        [SerializeField] private TextMeshProUGUI _wale;
        [SerializeField] private TextMeshProUGUI _score;

        [Space, SerializeField] Button _pause;

        private int _countWave;

        public void Init(int countWave)
        {
            UI.SaveArea(_saveArea);

            _countWave = countWave;
            
            NumberWale(0);
            Score(0);

            _pause.onClick.AddListener(() => _fSMGameplay.SuspendAndEnterIn(StateGameplay.PauseState));
        }

        public void NumberWale(int current)
        {
            _wale.text = $"Wale {current}/{_countWave}";
        }

        public void Score(int current)
        {
            _score.text = $"{current}";
        }
    }
}
