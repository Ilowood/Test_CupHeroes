using UnityEngine;

namespace Game
{
    public class MapLayer : MonoBehaviour
    {
        [SerializeField] private GameObject _camera;
        [field: SerializeField] public float ParallaxEffect { get; private set; }

        public float SizeX { get; private set; }

        private void Awake()
        {
            SizeX = GetComponent<SpriteRenderer>().bounds.size.x;
        }

        public void MapOffset(Vector3 offset)
        {
            var currentPosition = transform.position;
            var newPosition = new Vector3(
                currentPosition.x + offset.x * ParallaxEffect,
                currentPosition.y + offset.y * ParallaxEffect,
                currentPosition.z + offset.z * ParallaxEffect);

            var distance = Vector2.Distance(newPosition, _camera.transform.position);

            if (distance > SizeX)
            {
                newPosition.x += SizeX;
            }

            transform.position = newPosition;
        }
    }
}
