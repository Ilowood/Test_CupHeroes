using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game
{
    public class MovementSystem : IPausable
    {
        private readonly Transform _transform;
        private readonly MoveConfig _config;

        private float _currentSpeed;
        private float _maxSpeed;
        private bool _isPaused;

        public Transform Transform => _transform;
        public float CurrentSpeed => _currentSpeed;
        public bool IsPaused => _isPaused;
        public MoveConfig Config => _config;

        public MovementSystem(Transform transform, MoveConfig config)
        {
            _transform = transform;
            _config = config;
            _maxSpeed = config.RunProfile.Speed;
        }

        public event Action<float, float> SpeedChanged;

        public MovementBuilder CreateSequence() => new(this);
        public void Pause() => _isPaused = true;
        public void Resume() => _isPaused = false;  

        public UniTask Walk(Vector3 target) => Profiles.Trapezoid(this, target, _config.WalkProfile.Speed,
            _config.WalkProfile.AccelerationTime, _config.WalkProfile.DecelerationTime);

        public UniTask Run(Vector3 target) => Profiles.Trapezoid(this, target, _config.RunProfile.Speed,
            _config.RunProfile.AccelerationTime, _config.RunProfile.DecelerationTime);

        public async UniTask RunPhase(Vector3 targetPoint, float targetSpeed, float accelValue, float dir)
        {
            var distance = Vector3.Distance(_transform.position, targetPoint);
            var travelled = 0f;

            var token = _transform.GetCancellationTokenOnDestroy();

            while (travelled < distance - 1e-6f)
            {
                await WaitWhilePaused(token);

                _currentSpeed = Mathf.MoveTowards(_currentSpeed, targetSpeed, accelValue * Time.deltaTime);

                var step = _currentSpeed * Time.deltaTime;
                var stepClamped = step != 0 ? Mathf.Min(step, distance - travelled) : distance - travelled;

                _transform.position += new Vector3(dir * stepClamped, 0f, 0f);
                travelled += stepClamped;

                SpeedChanged?.Invoke(_currentSpeed, _maxSpeed);
                await UniTask.NextFrame(_transform.GetCancellationTokenOnDestroy());
            }

            _transform.position = targetPoint;
        }

        private async UniTask WaitWhilePaused(CancellationToken token)
        {
            while (_isPaused)
                await UniTask.NextFrame(token);
        }
    }
}
