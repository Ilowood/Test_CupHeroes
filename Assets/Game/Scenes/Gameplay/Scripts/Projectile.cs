using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game
{
    public class Projectile : MonoBehaviour
    {
        private CancellationTokenSource _cts;
        private UniTaskCompletionSource<bool> _collisionTcs;

        private int _damage;

        public void Launch(Vector3 direction, int damage)
        {
            _damage = damage;
            _cts = new CancellationTokenSource();
            _collisionTcs = new UniTaskCompletionSource<bool>();

            transform.GetComponent<Rigidbody2D>().AddForceX(transform.right.x * 140);
        }

        public void OnCollisionEnter2D(Collision2D collision)
        {
            collision.gameObject.GetComponent<EntityController>().GetDamage(_damage);

            _collisionTcs?.TrySetResult(true);
            _cts?.Cancel();
            Destroy(gameObject, 0.1f);
        }

        public UniTask<bool> WaitForCollisionAsync()
        {
            return _collisionTcs.Task;
        }

        private void OnDestroy()
        {
            _cts?.Cancel();
            _cts?.Dispose();
            _collisionTcs?.TrySetCanceled();
        }
    }
}
