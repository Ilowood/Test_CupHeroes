using UnityEngine;
using UnityEngine.UI;
using Untils;

namespace Game
{
    public class PauseView : BaseView
    {
        [SerializeField] private RectTransform _saveArea;

        [Header("Buttons")]
        [SerializeField] private Button _resume;
        [SerializeField] private Button _englishLanguage;
        [SerializeField] private Button _russianLanguage;

        public void Init(PauseState pauseState)
        {
            UI.SaveArea(_saveArea);

            _resume.onClick.AddListener(() => pauseState.ExitAndResume());
            _englishLanguage.onClick.AddListener(() => pauseState.SetEnglishLanguage());
            _russianLanguage.onClick.AddListener(() => pauseState.SetRussianLanguage());
        }
    }
}
