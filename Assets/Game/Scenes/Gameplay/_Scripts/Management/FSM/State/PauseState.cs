using UnityEngine.Localization.Settings;
using Untils;

namespace Game
{
    public class PauseState : IFSMState<StateGameplay>
    {
        private readonly FSMGameplay _fSM;
        private readonly PauseView _view;

        public PauseState(FSMGameplay fsm, PauseView view)
        {
            _fSM = fsm;
            _view = view;
            _view.Init(this);
        }

        public StateGameplay State => StateGameplay.PauseState;

        public void ExitAndResume() => _fSM.ExitAndResume();
        public void SetEnglishLanguage() => ChangeLanguage("en");
        public void SetRussianLanguage() => ChangeLanguage("ru");

        public void Enter()
        {
            _view.Enable();
        }

        public void Exit()
        {
            _view.Disable();
        }

        private void ChangeLanguage(string language)
        {
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.GetLocale(language);
        }
    }
}
