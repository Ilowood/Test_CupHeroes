using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(menuName = "Config/LevelConfig", fileName = "LevelConfig")]
    public class LevelConfig : ScriptableObject
    {
        [field: SerializeField] public List<Wale> Wave { get; private set; }
    }

    [Serializable]
    public class Wale
    {
        public Vector2[] SpawnOffsets { get => new Vector2[] 
            {
                new Vector2(1.0f, 0f),
                new Vector2(1.2f, 0.2f),
                new Vector2(1.2f, -0.2f)
            }; 
        }

        public float[] TargetOffsets { get => new float[] 
            {
                -2.0f,
                -2.0f,
                -2.0f
            }; 
        }

        [field: SerializeField] public List<EntityConfig> Enemies { get; private set; }
    }
}
