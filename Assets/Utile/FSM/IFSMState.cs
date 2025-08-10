using System;

namespace Untils
{
    public interface IFSMState<TEnum> where TEnum : Enum
    {
        void Enter();
        void Exit();
    }

    public interface IFSMStateExit<T> : IFSMState<T> where T : Enum
    {
        void ExitFSM();
    }
}