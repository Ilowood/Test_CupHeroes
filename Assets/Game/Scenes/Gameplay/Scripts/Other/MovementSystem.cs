using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game
{
    public class MovementSystem
    {
        private float _currentSpeed = 0;
        private float _speedAnimation = 0;

        private Transform _transform;
        private Animator _animator;

        private MoveConfig _moveConfig;
        private AnimationConfig _animationConfig;

        public MovementSystem(Transform transform, Animator animator, MoveConfig moveConfig, AnimationConfig animationConfig)
        {
            _transform = transform;
            _animator = animator;

            _moveConfig = moveConfig;
            _animationConfig = animationConfig;
        }

        public event Action<Vector2> OffsetEvent;

        public async UniTask SyncWalk(Vector3 targetPoint)
            => await SyncMoveTo(targetPoint, _moveConfig.WalkSpeed, _animationConfig.WalkAnimSpeed);
        public async UniTask SyncRun(Vector3 targetPoint)
            => await SyncMoveTo(targetPoint, _moveConfig.RunSpeed, _animationConfig.RunAnimSpeed);

        public async UniTask SyncMoveTo(Vector3 targetPoint, float speedMovement, float speedAnimation) 
            => await MoveTo(targetPoint, speedMovement, speedAnimation);

        private async UniTask MoveTo(Vector3 targetPosition, float speedMovement, float speedAnimation)
        {
            var transform = _transform.transform;
            var direction = Mathf.Sign(targetPosition.x - transform.position.x);

            while (Vector3.Distance(transform.position, targetPosition) >= 1e-2)
            {
                if (_currentSpeed < speedMovement)
                {
                    _currentSpeed = Mathf.MoveTowards(_currentSpeed, speedMovement, 1.5f * Time.deltaTime);
                }

                var translation = new Vector2(_currentSpeed * direction, 0) * Time.deltaTime;
                transform.position = new(transform.position.x + translation.x, transform.position.y, transform.position.z);

                _speedAnimation = NormalizeToSubRange(_currentSpeed, 0, speedMovement, 0, speedAnimation);
                _animator.SetFloat("Move", _speedAnimation);
                OffsetEvent?.Invoke(translation);

                await UniTask.NextFrame(/*this.GetCancellationTokenOnDestroy()*/);
            }

            _speedAnimation = 0;
            _currentSpeed = 0;
            _animator.SetFloat("Move", _speedAnimation);
        }

        private float NormalizeToSubRange(float value, float srcMin, float srcMax, float targetMin, float targetMax)
        {
            var normalized = Mathf.InverseLerp(srcMin, srcMax, value);
            return Mathf.Lerp(targetMin, targetMax, normalized);
        }
    }
}
