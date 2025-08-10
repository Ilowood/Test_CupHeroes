using System;
using Cysharp.Threading.Tasks;
using DamageNumbersPro;
using DG.Tweening;
using UnityEngine;

namespace Game
{
    public class EntityController : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private HealthBarView _healthBarView;

        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private GameObject _projectilePrefab;
        [SerializeField] private DamageNumber _prefabDamage;
        [SerializeField] private RectTransform _canvas;

        private int _currentHealth;
        private float _currentSpeed = 0;
        private int _currentDamage = 0;
        private EntityConfig _entityConfig;

        public bool IsLive;
        public event Action<Vector2> OffsetEvent;
        public event Action DeathEvent;

        public void Init(EntityConfig entityConfig)
        {
            _entityConfig = entityConfig;
            _currentHealth = _entityConfig.Health;
            _currentDamage = _entityConfig.Damage;
            IsLive = true;
            _healthBarView.Init(_currentHealth);
            _healthBarView.gameObject.SetActive(true);
        }

        #region IDamagable
        public void GetDamage(int damage)
        {
            var currentHealth = _currentHealth - damage;

            if (currentHealth > 0)
            {
                _currentHealth = currentHealth;
                _healthBarView.UpdateView(_currentHealth);

                _prefabDamage.Spawn(transform.position + new Vector3(0, 0.25f, 0), damage);
            }
            else
            {
                _prefabDamage.Spawn(transform.position + new Vector3(0, 0.25f, 0), damage);
                OnDeath();
            }
        }

        public void OnDeath()
        {
            IsLive = false;
            _animator.SetTrigger("Dead");
            _healthBarView.gameObject.SetActive(false);
            DeathEvent?.Invoke();
            Destroy(transform.gameObject, 2f);
        }

        public async UniTask Attack(Transform targetPosition)
        {
            _animator.SetTrigger("Attack");
            await OnAttack(targetPosition);
            await UniTask.Delay(1000);
        }

        public async UniTask OnAttack(Transform targetPosition)
        {
            await UniTask.Delay(1200);
            var projectileObj = Instantiate(_projectilePrefab, _spawnPoint.position, _spawnPoint.rotation);
            projectileObj.transform.rotation = _spawnPoint.rotation;
            var projectile = projectileObj.GetComponent<Projectile>();

            projectile.Launch(_spawnPoint.forward, _currentDamage);
            await projectile.WaitForCollisionAsync();
        }
        #endregion

        #region Move
        public async UniTask SyncMoveTo(Vector2 targetPoint, Vector2 speedMovement, Vector2 speedAnimation)
        {
            await MoveTo(targetPoint, speedMovement, speedAnimation);
        }

        private async UniTask MoveTo(Vector2 targetPosition, Vector2 speedMovement, Vector2 speedAnimation)
        {
            var direction = Mathf.Sign(targetPosition.x - transform.position.x);

            while (Vector2.Distance(transform.position, targetPosition) >= 1e-2)
            {
                if (_currentSpeed < speedMovement.y)
                {
                    _currentSpeed = Mathf.MoveTowards(_currentSpeed, speedMovement.y, 1.5f * Time.deltaTime);
                }

                var translation = new Vector2(_currentSpeed * direction, 0) * Time.deltaTime;
                transform.position = new(transform.position.x + translation.x, transform.position.y, transform.position.z);

                _animator.SetFloat("Move", NormalizeToSubRange(_currentSpeed, speedMovement.x, speedMovement.y, speedAnimation.x, speedAnimation.y));
                OffsetEvent?.Invoke(translation);

                await UniTask.NextFrame(this.GetCancellationTokenOnDestroy());
            }

            _currentSpeed = 0;
            _animator.SetFloat("Move", 0);
        }

        private float NormalizeToSubRange(float value, float srcMin, float srcMax, float targetMin, float targetMax)
        {
            var normalized = Mathf.InverseLerp(srcMin, srcMax, value);
            return Mathf.Lerp(targetMin, targetMax, normalized);
        }
        #endregion
    }
}
