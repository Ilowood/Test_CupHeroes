using System;

namespace Game
{
    public class HealthSystem
    {
        private int _currentHealth;
        private int _maxHealth;

        public int CurrentHealth => _currentHealth;
        public int MaxHealth => _maxHealth;
        public bool IsDead => _currentHealth <= 0;

        public HealthSystem(HealthConfig healthConfig)
        {
            _maxHealth = healthConfig.MaxHealth;
            _currentHealth = healthConfig.CurrentHealth;
        }

        public event Action<int> DamageTaken;
        public event Action Died; 

        public void TakeDamage(int damage)
        {
            var possibleHealth = _currentHealth - damage;
            
            if (possibleHealth > 0)
            {
                _currentHealth = possibleHealth;
                DamageTaken?.Invoke(_currentHealth);
            }
            else
            {
                OnDeath();
            }
        }

        public void OnDeath()
        {
            _currentHealth = 0;
            Died?.Invoke();
        }
    }
}
