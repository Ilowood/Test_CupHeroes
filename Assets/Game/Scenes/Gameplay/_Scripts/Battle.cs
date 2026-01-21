using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Game
{
    public class BattleOrder : IPausable
    {
        private readonly EntityController _player;
        private readonly List<EntityController> _enemies;
        private readonly CancellationToken _ct;

        private UniTaskCompletionSource _pauseSource;
        private bool _isPaused;

        private bool IsAnyEnemyAlive => _enemies.Any(e => e != null);
        public bool IsVictory => _player != null && _player.HealthSystem.CurrentHealth > 0;

        public BattleOrder(EntityController player)
        {
            _player = player;
        }

        public BattleOrder(EntityController player, List<EntityController> enemies)
        {
            _player = player;
            _enemies = enemies;

            _isPaused = false;
            _ct = new();
        }

        private async UniTask WaitIfPaused()
        {
            if (_isPaused)
            {
                await _pauseSource.Task.AttachExternalCancellation(_ct);
            }
        }

        public void Pause()
        {
            if (_isPaused) return;
            _isPaused = true;
            _pauseSource = new UniTaskCompletionSource();
        }

        public void Resume()
        {
            if (!_isPaused) return;
            _isPaused = false;
            _pauseSource?.TrySetResult();
        }

        public async UniTask Start()
        {
            while (_player.HealthSystem.CurrentHealth > 0 && IsAnyEnemyAlive)
            {
                await WaitIfPaused();
                await Round();
                await UniTask.Delay(200);
            }
        }

        private async UniTask Round()
        {
            await ActionEntity(_player);
            
            if (!IsAnyEnemyAlive) return;

            foreach (var enemy in _enemies)
            {
                if (enemy.HealthSystem.CurrentHealth > 0)
                {
                    await WaitIfPaused();
                    await ActionEntity(enemy);

                    if (_player.HealthSystem.CurrentHealth <= 0)
                    {
                        return;
                    }
                }
            }
        }

        private async UniTask ActionEntity(EntityController entity)
        {
            await WaitIfPaused();
            await entity.AttackSystem.Execute();
            await UniTask.Delay(100);
        }
    }
}
