using UnityEngine;
using Untils;

namespace Game
{
    public class AnimationSystem : IPausable
    {
        private float _moveSpeedAnimation = 0;

        private readonly Animator _animator;

        public AnimationSystem(Animator animator)
        {
            _animator = animator;
        }

        public void Pause()
        {
            _animator.speed = 0f;
        }

        public void Resume()
        {
            _animator.speed = 1f;
        }

        public void HandlerSpeedChanged(float currentSpeed, float maxSpeed)
        {
            _moveSpeedAnimation = GameMath.NormalizeToSubRange(currentSpeed, 0, maxSpeed, 0, 1);
            _animator.SetFloat(AnimatorParams.Move, _moveSpeedAnimation);
        }
    }
}
