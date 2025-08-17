using UnityEngine;
using Untils;

namespace Game
{
    public class AnimationSystem
    {
        private float _speedAnimation = 0;

        private readonly Animator _animator;

        public AnimationSystem(Animator animator)
        {
            _animator = animator;
        }

        public void HandlerSpeedChanged(float currentSpeed, float maxSpeed)
        {
            _speedAnimation = GameMath.NormalizeToSubRange(currentSpeed, 0, maxSpeed, 0, 1);
            _animator.SetFloat(AnimatorParams.Move, _speedAnimation);
        }
    }
}
