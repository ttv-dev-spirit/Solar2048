#nullable enable
using UnityEngine;

namespace Solar2048.UI.TransitionHandlers
{
    public class SimpleShowObjectHandler : MonoBehaviour, IShowTransitionHandler
    {
        public void OnShow()
        {
            gameObject.SetActive(true);
        }
    }
}