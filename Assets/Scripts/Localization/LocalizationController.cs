#nullable enable
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace Solar2048.Localization
{
    public sealed class LocalizationController
    {
        private readonly Dictionary<Language, Locale> _availableLocales = new();
        private readonly IReactiveProperty<bool> _isInitialized = new ReactiveProperty<bool>();

        public Language? SelectedLanguage { get; private set; }

        public IReadOnlyList<Language> AvailableLanguages => _availableLocales.Keys.ToList();
        public IReadOnlyReactiveProperty<bool> IsInitialized => _isInitialized;

        public LocalizationController()
        {
            InitializeLocalization();
        }

        public void SelectLanguage(Language language)
        {
            LocalizationSettings.SelectedLocale = GetLocale(language);
            SelectedLanguage = language;
        }

        private async void InitializeLocalization()
        {
            await LocalizationSettings.Instance.GetInitializationOperation().Task;
            var locales = LocalizationSettings.AvailableLocales.Locales;

            foreach (Locale locale in locales)
            {
                var language = new Language(locale.LocaleName);
                _availableLocales.Add(language, locale);
                if (locale == LocalizationSettings.SelectedLocale)
                {
                    SelectedLanguage = language;
                }
            }

            _isInitialized.Value = true;
        }

        private Locale GetLocale(Language language) => _availableLocales[language];
    }
}