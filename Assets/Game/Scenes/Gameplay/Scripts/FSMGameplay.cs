using Untils;

namespace Game
{
    public class FSMGameplay : FSMDictionary<StateGameplay>
    {
        public FSMGameplay()
        {
            States = new();
        }

        public void AddState(IFSMState<StateGameplay> state, StateGameplay key)
        {
            States.Add(key, state);
        }
    }
}
