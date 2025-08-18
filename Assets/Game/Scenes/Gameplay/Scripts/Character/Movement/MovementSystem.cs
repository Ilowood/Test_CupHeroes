using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game
{
    public class MovementSystem
    {
        private readonly Transform _transform;
        private readonly MoveConfig _config;

        private float _currentSpeed;
        private float _maxSpeed;

        public Transform Transform => _transform;
        public float CurrentSpeed => _currentSpeed;
        public MoveConfig Config => _config;

        public MovementSystem(Transform transform, MoveConfig config)
        {
            _transform = transform;
            _config = config;
            _maxSpeed = config.RunProfile.Speed;
        }
        public event Action<Vector2> OffsetEvent;
        public event Action<float, float> SpeedChanged;

        public MovementBuilder CreateSequence() => new(this);

        public UniTask Walk(Vector3 target) => Profiles.Trapezoid(this, target, _config.WalkProfile.Speed,
            _config.WalkProfile.AccelerationTime, _config.WalkProfile.DecelerationTime);

        public UniTask Run(Vector3 target) => Profiles.Trapezoid(this, target, _config.RunProfile.Speed,
            _config.RunProfile.AccelerationTime, _config.RunProfile.DecelerationTime);

        public async UniTask RunPhase(Vector3 targetPoint, float targetSpeed, float accelValue, float dir)
        {
            var distance = Vector3.Distance(_transform.position, targetPoint);
            var travelled = 0f;

            while (travelled < distance - 1e-2f)
            {
                _currentSpeed = Mathf.Abs(accelValue) > 1e-2f ? Mathf.MoveTowards(_currentSpeed, targetSpeed, accelValue * Time.deltaTime)
                    : targetSpeed;

                var step = _currentSpeed * Time.deltaTime;
                var stepClamped = Mathf.Min(step, distance - travelled);

                _transform.position += new Vector3(dir * stepClamped, 0f, 0f);
                travelled += stepClamped;

                OffsetEvent?.Invoke(new Vector2(dir * stepClamped, 0));
                SpeedChanged?.Invoke(_currentSpeed, _maxSpeed);

                await UniTask.NextFrame(_transform.GetCancellationTokenOnDestroy());
            }

            _transform.position = targetPoint;
        }
    }
}
