using System;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(menuName = "Config/EntityConfig", fileName = "EntityConfig")]
    public class EntityConfig : ScriptableObject
    {
        [field: Header("Параметры")]
        [field: SerializeField] public HealthConfig Health { get; private set; }
        [field: SerializeField] public MoveConfig Move { get; private set; }
        [field: SerializeField] public AnimationConfig Animation { get; private set; }

        [field: Header("Системы")]
        [field: SerializeReference] 
        [field: SerializeField] public BaseAction AttackSystem { get; private set; }

        [field: Header("Обьект")]
        [field: SerializeField, Space] public GameObject Prefab { get; private set; }
    }

    [Serializable]
    public class MoveConfig
    {
        [field: SerializeField] public MoveProfile WalkProfile { get; private set; }
        [field: SerializeField] public MoveProfile RunProfile { get; private set; }
    }

    [Serializable]
    public class MoveProfile
    {
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public float AccelerationTime { get; private set; }
        [field: SerializeField] public float DecelerationTime { get; private set; }
    }

    [Serializable]
    public class HealthConfig
    {
        [field: SerializeField] public int MaxHealth { get; private set; }
        [field: SerializeField] public int CurrentHealth { get; private set; }
    }
    
    [Serializable]
    public class AnimationConfig
    {
        [field: SerializeField] public float WalkAnimSpeed { get; private set; } = 0.5f;
        [field: SerializeField] public float RunAnimSpeed { get; private set; } = 1f;
    }
}
