using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(menuName = "Config")]
    public class LevelConfig : ScriptableObject
    {
        [field: SerializeField] public List<Wale> Wale { get; private set; }
    }

    [Serializable]
    public class Wale
    {
        [field: SerializeField] public List<EntityConfig> Enemys { get; private set; }
    }

    [Serializable]
    public class EntityConfig
    {
        [field: SerializeField] public int Health { get; private set; }
        [field: SerializeField] public int Damage { get; private set; }
    }
}
