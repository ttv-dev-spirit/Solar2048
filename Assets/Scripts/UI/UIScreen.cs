#nullable enable
using Solar2048.UI.TransitionHandlers;
using UnityEngine;
using Zenject;

namespace Solar2048.UI
{
    public abstract class UIScreen : MonoBehaviour, IUIScreen
    {
        private bool _isShown;
        private IShowTransitionHandler[]? _showTransitionHandlers;
        private IHideTransitionHandler[]? _hideTransitionHandlers;

        public bool IsShown => _isShown;

        [Inject]
        protected void Register(UIManager uiManager)
        {
            _showTransitionHandlers = GetComponents<IShowTransitionHandler>();
            _hideTransitionHandlers = GetComponents<IHideTransitionHandler>();
            uiManager.Register(this);
        }

        public void Show()
        {
            _isShown = true;
            HandleShowTransitions();
            OnShow();
        }

        public void Hide()
        {
            _isShown = false;
            OnHide();
            HandleHideTransitions();
        }

        protected abstract void OnShow();
        protected abstract void OnHide();

        private void HandleShowTransitions()
        {
            if (_showTransitionHandlers == null)
            {
                return;
            }

            foreach (IShowTransitionHandler showTransitionHandler in _showTransitionHandlers)
            {
                showTransitionHandler.OnShow();
            }
        }

        private void HandleHideTransitions()
        {
            if (_hideTransitionHandlers == null)
            {
                return;
            }

            foreach (IHideTransitionHandler hideTransitionHandler in _hideTransitionHandlers)
            {
                hideTransitionHandler.OnHide();
            }
        }
    }
}