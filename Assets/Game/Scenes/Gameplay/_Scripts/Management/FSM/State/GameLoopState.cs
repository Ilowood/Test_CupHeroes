using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Untils;

namespace Game
{
    public class GameLoopState : ISuspendFSMState<StateGameplay>
    {
        private readonly FSMGameplay _fsm;

        private readonly EntityController _playerController;
        private readonly WorldController _worldController;
        private readonly HUDView _hudView;

        private readonly GameplayConfig _gameplayConfig;

        private BattleOrder _battle;
        private List<EntityController> _enemies;

        public GameLoopState(FSMGameplay fsm, EntityController playerController, HUDView hudView, WorldController worldController, GameplayConfig gameplayConfig)
        {
            _fsm = fsm;

            _worldController = worldController;
            _hudView = hudView;    
            _playerController = playerController;
            _gameplayConfig = gameplayConfig;
        }

        public StateGameplay State => StateGameplay.GameLoopState;

        public void Enter()
        {
            AsyncEnter().Forget();
        }

        public void Suspend()
        {
            _battle.Pause();
            _playerController.Pause();
            _enemies?.Where(e => e != null).ToList().ForEach(e => e.Pause());
        }

        public void Resume()
        {
            _battle.Resume();
            _playerController.Resume();
            _enemies?.Where(e => e != null).ToList().ForEach(e => e.Resume());
        }

        public void Exit()
        {
            
        }

        private async UniTask AsyncEnter()
        {
            var levelConfig = _gameplayConfig.Level;
            _battle = new BattleOrder(_playerController);

            for (var i = 0; i != levelConfig.Wave.Count; i++)
            {
                _hudView.NumberWale(i + 1);
                _enemies = CreateWale(levelConfig.Wave[i]);

                await MoveToPoint(_enemies, levelConfig.Wave[i].TargetOffsets);

                if (i == 0)
                {
                    _playerController.HealthBar.Show();
                    _hudView.EnableHUD();
                }

                _enemies.ForEach(x => x.HealthBar.Show());
                
                _battle = new BattleOrder(_playerController, _enemies);
                await _battle.Start();

                if (!_battle.IsVictory)
                {
                    _fsm.EnterIn(StateGameplay.DefeatState);
                    return;
                }

                if (i != levelConfig.Wave.Count - 1)
                {
                    var point = new Vector3(
                        _playerController.transform.position.x + _worldController.LastMapLayer.SizeX * _worldController.LastMapLayer.ParallaxEffect, 
                        _playerController.transform.position.y, 
                        _playerController.transform.position.z);

                    await _playerController.MovementSystem.Run(point);
                }
            }

            if (_battle.IsVictory)
            {
                _fsm.EnterIn(StateGameplay.VictoryState);
            }
            else
            {
                _fsm.EnterIn(StateGameplay.DefeatState);
            }
        }

        private List<EntityController> CreateWale(Wale wave)
        {
            var enemies = new List<EntityController>();
            var offsetPoints = wave.SpawnOffsets;

            for (var i = 0; i < wave.Enemies.Count; i++)
            {
                var enemyObject = _worldController.SpawnRightOfCamera(wave.Enemies[i].Prefab, offsetPoints[i]);
                var enemyController = enemyObject.GetComponent<EntityController>();
                enemyController.Init(wave.Enemies[i]);

                enemies.Add(enemyController);
            }

            return enemies;
        }

        private async UniTask MoveToPoint(List<EntityController> enemies, float[] xOffsets)
        {
            var moveTasks = new UniTask[enemies.Count];
            for (var i = 0; i < enemies.Count; i++)
            {
                var point = new Vector3(enemies[i].transform.position.x + xOffsets[i], enemies[i].transform.position.y, enemies[i].transform.position.z);
                moveTasks[i] = enemies[i].MovementSystem.Walk(point);
            }

            await UniTask.WhenAll(moveTasks);
        }
    }
}
