using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Untils;

namespace Game
{
    public class Gameplay : MonoBehaviour
    {
        [Header("Screens")]
        [SerializeField] private RectTransform _saveScreen;

        [Header("Controllers")]
        [SerializeField] private EntityController _playerController;
        [SerializeField] private CameraController _cameraController;
        [SerializeField] private WorldController _worldController;
        [SerializeField] private UIController _UIController;

        [Header("Level settings")]
        [SerializeField] private Transform _startPoint;
        [SerializeField] private List<Transform> _walePoint;
        [SerializeField] private Transform _tEnemy;

        [Header("Menu")]
        [SerializeField] private GameObject _win;
        [SerializeField] private GameObject _lose;

        [Header("Prefabs")]
        [SerializeField] private Transform _containerEntities;
        [SerializeField] private GameObject _entityPrefab;

        [Header("Data")]
        [SerializeField] private LevelConfig _levelConfig;
        [SerializeField] private EntityConfig _playerConfig;

        private void Start()
        {
            var fsm = new FSMGameplay();
            fsm.AddState(new InitState(fsm, _saveScreen), StateGameplay.Init);
            fsm.AddState(new CinematicState(fsm, _playerController, _cameraController, _startPoint), StateGameplay.Intro);

            fsm.AddState(new GameState(fsm, _playerController, _worldController, _UIController, _walePoint, _tEnemy, _containerEntities, _entityPrefab,
                _levelConfig, _playerConfig), StateGameplay.StartGame);

            fsm.AddState(new LoseState(_lose), StateGameplay.LoseState);
            fsm.AddState(new WinState(_win), StateGameplay.WinState);

            fsm.EnterIn(StateGameplay.Init);
        }

        public List<EntityController> CreateEnemies(Wale waleConfig)
        {
            var enemies = new List<EntityController>();

            for (var i = 0; i < waleConfig.Enemys.Count; i++)
            {
                var spawnPoint = new Vector3(_playerController.transform.position.x + 6, _playerController.transform.position.y, _playerController.transform.position.z);
                var enemyClone = Instantiate(_entityPrefab, spawnPoint, Quaternion.identity, _containerEntities);
                var controller = enemyClone.GetComponent<EntityController>();
                controller.Init(waleConfig.Enemys[i]);

                enemies.Add(controller);
            }

            return enemies;
        }

        public void MainSceneLoad()
        {
            SceneManager.LoadScene(0);
        }
    }
}
