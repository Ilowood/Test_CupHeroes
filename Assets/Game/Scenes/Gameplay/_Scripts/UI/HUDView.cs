using System;
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

        [Space, SerializeField] private GameObject _waveContainer;
        [SerializeField] private TextMeshProUGUI _waveCounter;
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

        public void DisableHUD()
        {
            _waveContainer.SetActive(false);
            _score.gameObject.SetActive(false);
        }

        public void EnableHUD()
        {
            _waveContainer.SetActive(true);
            _score.gameObject.SetActive(true);
        }

        public void NumberWale(int current)
        {
            _waveCounter.text = $" {current}/{_countWave}";
        }

        public void Score(int current)
        {
            _score.text = $"{current}";
        }
    }
}
