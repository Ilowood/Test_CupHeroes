using System;

namespace Untils
{
    public interface IFSMState<TEnum> where TEnum : Enum
    {
        public TEnum State { get; }
        
        public void Enter();
        public void Exit();
    }
}