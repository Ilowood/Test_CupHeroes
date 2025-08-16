using UnityEngine;

namespace Game
{
    public class EntityController : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        private EntityConfig _entityConfig;


        public MovementSystem MovementSystem { get; private set; }

        public void Init(EntityConfig entityConfig)
        {
            _entityConfig = entityConfig;
            MovementSystem = new MovementSystem(transform, _animator, _entityConfig.Move, _entityConfig.Animation);
        }
    }
}
