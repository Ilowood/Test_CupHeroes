using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Untils;

namespace Game
{
    public class GameState : IFSMState<StateGameplay>
    {
        private readonly FSMGameplay _fsm;

        private readonly EntityController _playerController;
        private readonly WorldController _worldController;
        private readonly UIController _uIController;
        private readonly List<Transform> _walePoints;

        private readonly Transform _containerEntities;
         private readonly Transform _enemyPoint;
        private readonly GameObject _prefabEntity;

        private readonly LevelConfig _levelConfig;
        private readonly EntityConfig _playerConfig;

        private LevelInfo _levelInfo;

        public StateGameplay State => throw new System.NotImplementedException();

        public GameState(FSMGameplay fsm, EntityController playerController, WorldController mapController, UIController uIController,
            List<Transform> walePoints, Transform enemyPoint,
            Transform _containerEntities, GameObject prefabEntity,
            LevelConfig levelConfig, EntityConfig playerConfig)
        {
            _fsm = fsm;

            _prefabEntity = prefabEntity;

            _playerController = playerController;
            _worldController = mapController;
            _uIController = uIController;
            _walePoints = walePoints;
            _enemyPoint = enemyPoint;

            _levelConfig = levelConfig;
            _playerConfig = playerConfig;
        }

        public async void Enter()
        {
            _playerController.Init(_playerConfig);
            _worldController.Init(_playerController);

            _uIController.UpdateWale(1, _levelConfig.Wale.Count);
            await Battle(_levelConfig.Wale[0]);

            for (var i = 1; i < _levelConfig.Wale.Count; i++)
            {
                _uIController.UpdateWale(i+1, _levelConfig.Wale.Count);

                var indexPosition = i % _walePoints.Count;
                var distanse = Vector2.Distance(_walePoints[indexPosition].position, _walePoints[indexPosition].position * 1.3f);
                var position = _walePoints[indexPosition].position * 1.3f;
                position.x += distanse * 0.3f;
                await _playerController.MovementSystem.Move(position, 2).SuppressCancellationThrow();

                await Battle(_levelConfig.Wale[i]);
            }

            if (_playerController != null)
            {
                _fsm.EnterIn(StateGameplay.WinState);
            }
            else
            {
                _fsm.EnterIn(StateGameplay.LoseState);
            }

            Debug.Log($"Уровень пройден!");
        }

        public void Exit()
        {
            _worldController.Deinit(_playerController);
        }

        private async UniTask Battle(Wale wale)
        {
            _levelInfo = new LevelInfo();
            var enemies = CreateEnemies(wale);

            var moveTasks = new UniTask[enemies.Count];
            for (var i = 0; i < enemies.Count; i++)
            {
                var position = enemies[i].transform.position;
                Debug.Log($"Точка спавна {position}");
                // position.x -= 2;
                var position1 = new Vector3(_enemyPoint.position.x, position.y, position.z);
                Debug.Log($"Точка движения {position1}");
                // moveTasks[i] = enemies[i].SyncMoveTo(position1, new(0, 1), new(0, 0.5f));
            }

            await UniTask.WhenAll(moveTasks);

            // var battleOrder = new Battle(_playerController, enemies);
            // await battleOrder.Start();

            Debug.Log($"Волна пройдена");
        }

        private List<EntityController> CreateEnemies(Wale waleConfig)
        {
            var enemies = new List<EntityController>();

            for (var i = 0; i < waleConfig.Enemys.Count; i++)
            {
                var enemyClone = _worldController.SpawnRightOfCamera(_prefabEntity, _containerEntities);
                var controller = enemyClone.GetComponent<EntityController>();
                controller.Init(waleConfig.Enemys[i]);

                enemies.Add(controller);
                // enemies.ForEach(x => x.DeathEvent += AddScore);
            }

            return enemies;
        }

        private void AddScore()
        {
            _levelInfo.Score++;
            _uIController.UpdateScore(_levelInfo.Score);
        }
    }
}
