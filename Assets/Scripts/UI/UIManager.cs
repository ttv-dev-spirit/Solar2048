#nullable enable
using System;
using System.Collections.Generic;
using Solar2048.StaticData;
using UniRx;
using UnityEngine;

namespace Solar2048.UI
{
    public sealed class UIManager
    {
        private readonly Dictionary<Type, IUIScreen> _screens = new();

        private readonly MouseOverObserver _mouseOverObserver;
        private readonly UIManagerSettings _settings;

        public IReadOnlyReactiveProperty<bool> IsMouseOver => _mouseOverObserver.IsMouseOver;
        public LayerMask LayerMask => _settings.LayerMask;

        public UIManager(StaticDataProvider staticDataProvider, MouseOverObserver mouseOverObserver)
        {
            _settings = staticDataProvider.UIManagerSettings;
            _mouseOverObserver = mouseOverObserver;
        }

        public void Register(IUIScreen screen)
        {
            if (_screens.TryGetValue(screen.GetType(), out IUIScreen? registered))
            {
                var registeredScreen = (MonoBehaviour)registered;
                var screenToRegister = (MonoBehaviour)screen;
                Debug.LogError(
                    $"Trying to register screen {screenToRegister.name} of type {screen.GetType().Name}, but {registeredScreen.name} of same type is already registered",
                    registeredScreen);
                return;
            }

            _screens[screen.GetType()] = screen;
            screen.Hide();
        }

        public T GetScreen<T>() where T : class, IUIScreen =>
            _screens[typeof(T)] as T ?? throw new InvalidOperationException();

        public void HideAll()
        {
            foreach (IUIScreen uiScreen in _screens.Values)
            {
                uiScreen.Hide();
            }
        }
    }
}