using System;
using UnityEngine;

namespace Game
{
    public class EntityConfig
    {
        public MoveConfig Move { get; private set; } = new();
        public AnimationConfig Animation { get; private set; } = new();
    }

    public class MoveConfig
    {
        public float WalkSpeed { get; private set; } = 0.5f;
        public float RunSpeed { get; private set; } = 1f;
    }
    
    public class AnimationConfig
    {
        public float WalkAnimSpeed { get; private set; } = 0.5f;
        public float RunAnimSpeed { get; private set; } = 1f;
    }
}
