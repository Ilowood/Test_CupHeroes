using UnityEngine;
using Cysharp.Threading.Tasks;

namespace Game
{
    public abstract class BaseAction : ScriptableObject
    {
        protected EntityController _entity;

        public BaseAction Init(EntityController entity)
        {
            _entity = entity;
            return this;
        }

        public abstract UniTask Execute();
    }
}
