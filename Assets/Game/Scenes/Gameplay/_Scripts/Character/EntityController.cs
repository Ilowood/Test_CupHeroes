using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class EntityController : MonoBehaviour, IPausable
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private HealthBar _healthBar;

        private List<IPausable> _pausablesSystems;
        private EntityConfig _entityConfig;

        public HealthSystem HealthSystem { get; private set; }
        public MovementSystem MovementSystem { get; private set; }
        public AnimationSystem AnimationSystem { get; private set; }
        public BaseAction AttackSystem { get; private set; }

        public HealthBar HealthBar => _healthBar;

        public void Init(EntityConfig entityConfig)
        {
            _entityConfig = entityConfig;

            HealthSystem = new HealthSystem(_entityConfig.Health);
            MovementSystem = new MovementSystem(transform, _entityConfig.Move);
            AnimationSystem = new AnimationSystem(_animator);

            AttackSystem = _entityConfig.AttackSystem.Init(this);

            _pausablesSystems = new() { MovementSystem, AnimationSystem };

            InitView();
            BindSystems();
        }

        public void Pause()
        {
            _pausablesSystems.ForEach(x => x.Pause());
        }

        public void Resume()
        {
            _pausablesSystems.ForEach(x => x.Resume());
        }

        private void InitView()
        {
            _healthBar.Init(HealthSystem.MaxHealth);
        }

        private void Deinit()
        {
            HealthSystem.DamageTaken -= _healthBar.HandleDamageTaken;
            HealthSystem.Died -= OnDestroy;
            MovementSystem.SpeedChanged -= AnimationSystem.HandlerSpeedChanged;
        }

        private void BindSystems()
        {
            HealthSystem.DamageTaken += _healthBar.HandleDamageTaken;
            HealthSystem.Died += OnDestroy;
            MovementSystem.SpeedChanged += AnimationSystem.HandlerSpeedChanged;
        }

        private void OnDestroy()
        {
            Deinit();
            Destroy(gameObject);
        }
    }
}
