using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private float _timeDamageEffect = 0.3f;
        [SerializeField] private float _timeDelayEffect = 0.15f;

        [Space, SerializeField] private Slider _mainSlider;
        [SerializeField] private Slider _sliderEffects;

        public void Init(int maxHealth)
        {
            _mainSlider.maxValue = maxHealth;
            _sliderEffects.maxValue = maxHealth;

            _mainSlider.value = maxHealth;
            _sliderEffects.value = maxHealth;
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void HandleDamageTaken(int currentHealth)
        {
            _mainSlider.value = currentHealth;
            DamageEffect(currentHealth);
        }

        private void DamageEffect(int targetValue)
        {
            _sliderEffects.DOKill();
            _sliderEffects.DOValue(targetValue, _timeDamageEffect).SetDelay(_timeDelayEffect);
        }
    }
}
