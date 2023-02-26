#nullable enable
using System;
using UniRx;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

namespace Solar2048.Localization.UI
{
    public sealed class LocalizationButton : MonoBehaviour
    {
        private Subject<Locale> _onClicked = new Subject<Locale>();
        private Locale _locale = null!;

        [SerializeField]
        private Button _button;

        [SerializeField]
        private LocalizeStringEvent _localizeString = null!;

        public IObservable<Locale> OnClicked => _onClicked;

        private void Awake()
        {
            _button.onClick.AddListener(ButtonClickedHandler);
        }

        public void Show(Locale locale)
        {
            _locale = locale;
            Debug.Log(locale.LocaleName);
            _localizeString.SetEntry(locale.LocaleName);
        }

        private void ButtonClickedHandler() => _onClicked.OnNext(_locale);
    }
}