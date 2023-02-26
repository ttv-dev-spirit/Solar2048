#nullable enable
using System;
using UniRx;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

namespace Solar2048.Localization.UI
{
    public sealed class LocalizationButton : MonoBehaviour
    {
        private Subject<Language> _onClicked = new();
        private Language _language = null!;

        [SerializeField]
        private Button _button = null!;

        [SerializeField]
        private LocalizeStringEvent _localizeString = null!;

        public IObservable<Language> OnClicked => _onClicked;

        private void Awake()
        {
            _button.onClick.AddListener(ButtonClickedHandler);
        }

        public void Show(Language language)
        {
            _language = language;
            _localizeString.SetEntry(_language.Name);
        }

        private void ButtonClickedHandler() => _onClicked.OnNext(_language);
    }
}