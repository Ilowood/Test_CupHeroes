using TMPro;
using UnityEngine;

namespace Game
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private RectTransform _saveArea;
        [SerializeField] private TextMeshProUGUI _wale;
        [SerializeField] private TextMeshProUGUI _score;

        public RectTransform SaveArea => _saveArea;

        public void UpdateWale(int current, int max)
        {
            _wale.text = $"Wale {current}/{max}";
        }

        public void UpdateScore(int current)
        {
            _score.text = $"{current}";
        }
    }
}
