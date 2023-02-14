#nullable enable
using UnityEngine;

namespace Solar2048.UI.TransitionHandlers
{
    public class SimpleHideObjectHandler : MonoBehaviour, IHideTransitionHandler
    {
        public void OnHide()
        {
            gameObject.SetActive(false);
        }
    }
}