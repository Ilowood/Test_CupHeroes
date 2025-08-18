using System;
using UnityEngine;

namespace Game
{
    public class EntityConfig
    {
        public MoveConfig Move { get; private set; }
        public AnimationConfig Animation { get; private set; }

        public EntityConfig(MoveConfig moveConfig, AnimationConfig animationConfig)
        {
            Move = moveConfig;
            Animation = animationConfig;
        }
    }

    public class MoveConfig
    {
        public MoveProfile WalkProfile { get; private set; }
        public MoveProfile RunProfile { get; private set; }

        public MoveConfig(MoveProfile walkProfile, MoveProfile runProfile)
        {
            WalkProfile = walkProfile;
            RunProfile = runProfile;
        }
    }

    public class MoveProfile
    {
        public float Speed { get; private set; }
        public float AccelerationTime { get; private set; }
        public float DecelerationTime { get; private set; }

        public MoveProfile(float speed, float accelerationTime, float decelerationTime)
        {
            Speed = speed;
            AccelerationTime = accelerationTime;
            DecelerationTime = decelerationTime;
        }
    }
    
    public class AnimationConfig
    {
        public float WalkAnimSpeed { get; private set; } = 0.5f;
        public float RunAnimSpeed { get; private set; } = 1f;
    }
}
