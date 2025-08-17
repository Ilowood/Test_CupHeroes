using UnityEngine;

namespace Game
{
    public class EntityController : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        private EntityConfig _entityConfig;


        public MovementSystem MovementSystem { get; private set; }
        public AnimationSystem AnimationSystem { get; private set; }

        public EntityController Init(EntityConfig entityConfig)
        {
            _entityConfig = entityConfig;

            MovementSystem = new MovementSystem(transform, _entityConfig.Move);
            AnimationSystem = new AnimationSystem(_animator);

            return this;
        }

        public void A()
        {
            MovementSystem.SpeedChanged += AnimationSystem.HandlerSpeedChanged;
        }
    }
}
