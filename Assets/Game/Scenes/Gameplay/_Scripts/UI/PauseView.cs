using UnityEngine;
using UnityEngine.UI;
using Untils;
using Zenject;

namespace Game
{
    public class PauseView : BaseView
    {
        [Inject] private FSMGameplay _fSMGameplay;

        [SerializeField] private RectTransform _saveArea;
        [SerializeField] Button _resume;

        public void Init()
        {
            UI.SaveArea(_saveArea);
            _resume.onClick.AddListener(() => _fSMGameplay.ExitAndResume());
        }
    }
}
