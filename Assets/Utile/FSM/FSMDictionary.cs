using System;
using System.Collections.Generic;

namespace Untils
{
    public abstract class FSMDictionary<TEnum> : IFSM<TEnum, Dictionary<TEnum, IFSMState<TEnum>>> where TEnum : Enum
    {
        public Dictionary<TEnum, IFSMState<TEnum>> States { get; protected set; }
        public IFSMState<TEnum> CurrentState { get; protected set; }

        /// <summary>
        /// Переход в стадию
        /// </summary>
        public virtual void EnterIn(TEnum state)
        {
            if (States.TryGetValue(state, out IFSMState<TEnum> stObj))
            {
                CurrentState?.Exit();
                CurrentState = stObj;
                CurrentState?.Enter();
            }
        }

        /// <summary>
        /// Выход из FSM
        /// </summary>
        public virtual void Exit<TInterface>(TEnum state) where TInterface : IFSMStateExit<TEnum>
        {
            if (States.TryGetValue(state, out IFSMState<TEnum> stObj))
            {
                CurrentState = null;
                var stateModifed = stObj as IFSMStateExit<TEnum>;
                stateModifed?.ExitFSM();
            }
        }
    }
}
