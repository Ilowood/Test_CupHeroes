using TMPro;
using UnityEngine;

namespace Game
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _wale;
        [SerializeField] private TextMeshProUGUI _score;

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
