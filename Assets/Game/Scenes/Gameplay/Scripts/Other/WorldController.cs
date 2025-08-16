using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Game
{
    public class WorldController : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private List<MapLayer> _mapLayers;
        [SerializeField] private Transform _startPoint;

        public Vector3 StartPosition => _startPoint.position;

        public void Init(EntityController entityController)
        {
            for (var i = 0; i < _mapLayers.Count; i++)
            {
                entityController.MovementSystem.OffsetEvent += _mapLayers[i].MapOffset;
            }
        }

        public void Deinit(EntityController entityController)
        {
            for (var i = 0; i < _mapLayers.Count; i++)
            {
                entityController.MovementSystem.OffsetEvent += _mapLayers[i].MapOffset;
            }
        }

        public GameObject SpawnRightOfCamera(GameObject _prefabToSpawn, Transform parent)
        {
            var rightBound = GetCameraRightBound();
            var spawnPosition = new Vector3(rightBound + 1.5f, 0f, 0);
            
            return Instantiate(_prefabToSpawn, spawnPosition, Quaternion.identity, parent);
        }

        private float GetCameraRightBound()
        {
            return _camera.transform.position.x + _camera.orthographicSize * _camera.aspect;
        }
    }
}
