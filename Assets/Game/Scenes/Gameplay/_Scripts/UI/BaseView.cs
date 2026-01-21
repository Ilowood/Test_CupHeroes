using UnityEngine;

namespace Game
{
    public abstract class BaseView : MonoBehaviour
    {
        public virtual void Enable()
        {
            gameObject.SetActive(true);
        } 

        public virtual void Disable()
        {
            gameObject.SetActive(false);
        }
    }
}
