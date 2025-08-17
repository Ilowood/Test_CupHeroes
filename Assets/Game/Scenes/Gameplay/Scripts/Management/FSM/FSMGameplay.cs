using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Untils;
using Zenject;

namespace Game
{
    public class FSMGameplay : FSMDictionary<StateGameplay>
    {
        [Inject]
        public void Init(IEnumerable<IFSMState<StateGameplay>> states)
        {
            States = states.ToDictionary(x => x.State, x => x);
        }

        public override void EnterIn(StateGameplay state)
        {
            Debug.Log($"[FSMGameplay] {CurrentState} -> {state}");
            base.EnterIn(state);
        }
    }
}
