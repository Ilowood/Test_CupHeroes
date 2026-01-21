using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private int _damage;

        private UniTaskCompletionSource _tcs;
        private bool _isCompleted;

        public async UniTask Init(float speed, float lifeTime)
        {
            _tcs = new UniTaskCompletionSource();
            _isCompleted = false;

            _rigidbody.AddForceX(transform.right.x * speed, ForceMode2D.Impulse);
            WaitLifetime(lifeTime).Forget();

            await _tcs.Task;
        }

        private async UniTaskVoid WaitLifetime(float lifeTime)
        {
            await UniTask.Delay(System.TimeSpan.FromSeconds(lifeTime), cancellationToken: this.GetCancellationTokenOnDestroy());
            Complete();
        }

        public void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out EntityController entity))
            {
                entity.HealthSystem.TakeDamage(_damage);
            }

            Complete();
        }

        private void Complete()
        {
            if (_isCompleted) return;

            _isCompleted = true;
            _tcs?.TrySetResult();

            Destroy(gameObject);
        }
    }
}
