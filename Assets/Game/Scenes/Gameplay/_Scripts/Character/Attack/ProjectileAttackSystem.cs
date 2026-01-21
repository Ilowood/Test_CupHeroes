using UnityEngine;
using Cysharp.Threading.Tasks;

namespace Game
{
    [CreateAssetMenu(fileName = "ProjectileAttackSystem", menuName = "Attacks/Projectile")]
    public class ProjectileAttackSystem : BaseAction
    {
        [SerializeField] private Projectile _projectilePrefab;
        [SerializeField] private float _speed;
        [SerializeField] private float _lifeTime;

        public override async UniTask Execute()
        {
            var transform = _entity.gameObject.transform;
            var projectile = Instantiate(_projectilePrefab, transform.position, transform.rotation);
            
            await projectile.Init(_speed, _lifeTime);
        }
    }
}
