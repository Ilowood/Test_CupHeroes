using UnityEngine;

namespace Game
{
    public class MapLayer : MonoBehaviour
    {
        [SerializeField] private GameObject _camera;
        [SerializeField] private float _parallaxEffect;

        private float _sizeX;

        private void Start()
        {
            _sizeX = GetComponent<SpriteRenderer>().bounds.size.x;
        }

        public void MapOffset(Vector2 playerOffset)
        {
            var currentPosition = transform.position;
            var newPosition = new Vector3(
                currentPosition.x + playerOffset.x * _parallaxEffect,
                currentPosition.y + playerOffset.y * _parallaxEffect,
                currentPosition.z);

            var distance = Vector2.Distance(newPosition, _camera.transform.position);

            if (distance > _sizeX)
            {
                newPosition.x += _sizeX;
            }

            transform.position = newPosition;
        }
    }
}
