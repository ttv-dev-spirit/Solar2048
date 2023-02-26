#nullable enable
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace Solar2048.Localization
{
    public sealed class LocalizationController
    {
        private readonly Dictionary<Language, Locale> _availableLocales = new();

        public Language SelectedLanguage { get; private set; }

        public IReadOnlyList<Language> AvailableLanguages => _availableLocales.Keys.ToList();

        public LocalizationController()
        {
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
        }

        public void SelectLanguage(Language language)
        {
            LocalizationSettings.SelectedLocale = GetLocale(language);
            SelectedLanguage = language;
        }

        private Locale GetLocale(Language language) => _availableLocales[language];
    }
}