using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class WorldController : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private List<MapLayer> _mapLayers;
        [SerializeField] private Transform _enableCameraPoint;
        [SerializeField] private Transform _containerEnemies;

        public Vector3 EnableCameraPoint => _enableCameraPoint.position;
        public MapLayer LastMapLayer => _mapLayers[_mapLayers.Count - 1];

        public void Init(CameraTracker cameraTracker)
        {
            for (var i = 0; i < _mapLayers.Count; i++)
            {
                cameraTracker.OnCameraOffsetChanged += _mapLayers[i].MapOffset;
            }
        }

        public void Deinit(CameraTracker cameraTracker)
        {
            for (var i = 0; i < _mapLayers.Count; i++)
            {
                cameraTracker.OnCameraOffsetChanged -= _mapLayers[i].MapOffset;
            }
        }

        public GameObject SpawnRightOfCamera(GameObject _prefabToSpawn, Vector2 spawnOffset)
        {
            var rightBound = GetCameraRightBoundX();
            var spawnPosition = new Vector3(rightBound + spawnOffset.x, 0f + spawnOffset.y, 0);

            return Instantiate(_prefabToSpawn, spawnPosition, _prefabToSpawn.transform.rotation, _containerEnemies);
        }

        public float GetCameraRightBoundX()
        {
            return _camera.transform.position.x + _camera.orthographicSize * _camera.aspect;
        }
    }
}
