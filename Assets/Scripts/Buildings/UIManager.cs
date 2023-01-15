#nullable enable
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

namespace Solar2048.Buildings
{
    public sealed class UIManager
    {
        private readonly List<IUIScreen> _screens = new();

        private readonly MouseOverObserver _mouseOverObserver;
        private readonly UIManagerSettings _settings;

        public IReadOnlyReactiveProperty<bool> IsMouseOver => _mouseOverObserver.IsMouseOver;
        public LayerMask LayerMask => _settings.LayerMask;

        public UIManager(UIManagerSettings uiManagerSettings, MouseOverObserver mouseOverObserver)
        {
            _settings = uiManagerSettings;
            _mouseOverObserver = mouseOverObserver;
        }

        public void Register(IUIScreen screen)
        {
            _screens.Add(screen);
            screen.Hide();
        }

        public T GetScreen<T>() where T : IUIScreen => (T)_screens.First(screen => screen is T);

        public void HideAll()
        {
            foreach (IUIScreen uiScreen in _screens)
            {
                uiScreen.Hide();
            }
        }
    }
}