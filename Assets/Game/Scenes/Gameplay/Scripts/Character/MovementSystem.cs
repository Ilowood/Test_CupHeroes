using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game
{
    public class MovementSystem
    {
        private float _currentSpeed;
        private float _maxSpeed;

        private Transform _transform;
        private MoveConfig _moveConfig;

        public MovementSystem(Transform transform, MoveConfig moveConfig)
        {
            _transform = transform;
            _moveConfig = moveConfig;

            _currentSpeed = 0;
            _maxSpeed = _moveConfig.RunSpeed;
        }

        public event Action<Vector2> OffsetEvent;
        public event Action<float, float> SpeedChanged;

        public async UniTask Walk(Vector3 targetPoint)
            => await MoveTo(targetPoint, _moveConfig.WalkSpeed);
        public async UniTask Run(Vector3 targetPoint)
            => await MoveTo(targetPoint, _moveConfig.RunSpeed);

        public async UniTask Move(Vector3 targetPoint, float maxSpeed)
        {
            if (maxSpeed > _maxSpeed)
            {
                throw new ArgumentException($"Attempted to set speed {maxSpeed}, which exceeds the maximum allowed value {_maxSpeed}.");
            }

            await MoveTo(targetPoint, maxSpeed);
        }

        private async UniTask MoveTo(Vector3 targetPosition, float currentMaxSpeed)
        {
            var transform = _transform.transform;
            var direction = Mathf.Sign(targetPosition.x - transform.position.x);

            while (Vector3.Distance(transform.position, targetPosition) >= 1e-2)
            {
                if (_currentSpeed < currentMaxSpeed)
                {
                    _currentSpeed = Mathf.MoveTowards(_currentSpeed, currentMaxSpeed, 1.5f * Time.deltaTime);
                }

                var translation = new Vector2(_currentSpeed * direction, 0) * Time.deltaTime;
                transform.position = new(transform.position.x + translation.x, transform.position.y, transform.position.z);

                OffsetEvent?.Invoke(translation);
                SpeedChanged?.Invoke(_currentSpeed, _maxSpeed);

                await UniTask.NextFrame(_transform.GetCancellationTokenOnDestroy());
            }

            _currentSpeed = 0;
            SpeedChanged?.Invoke(_currentSpeed, _maxSpeed);
        }
    }
}
