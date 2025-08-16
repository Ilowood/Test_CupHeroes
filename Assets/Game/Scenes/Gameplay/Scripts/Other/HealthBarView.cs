using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class HealthBarView : MonoBehaviour
    {
        [SerializeField] private float _timeDamageEffect = 0.3f;
        [SerializeField] private float _timeDelayEffect = 0.15f;
        [SerializeField] private Slider _mainSlider;
        [SerializeField] private Slider _sliderEffects;

        public void Init(int maxHealth)
        {
            _mainSlider.maxValue = maxHealth;
            _sliderEffects.maxValue = maxHealth;

            _mainSlider.value = maxHealth;
            _sliderEffects.value = maxHealth;
        }

        public void UpdateView(float currentHealth)
        {
            _mainSlider.value = currentHealth;
            DamageEffect(currentHealth);
        }

        private void DamageEffect(float targetValue)
        {
            Debug.Log(1);
            _sliderEffects.DOKill();
            _sliderEffects.DOValue(targetValue, _timeDamageEffect).SetDelay(_timeDelayEffect);
        }
    }
}
