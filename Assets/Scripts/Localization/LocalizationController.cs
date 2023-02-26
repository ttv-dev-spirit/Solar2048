#nullable enable
using System.Collections.Generic;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace Solar2048.Localization
{
    public sealed class LocalizationController
    {
        private readonly List<Locale> _availableLocales;

        public IReadOnlyList<Locale> AvailableLocales => _availableLocales;

        public LocalizationController()
        {
            _availableLocales = LocalizationSettings.AvailableLocales.Locales;
        }

        public void SelectLocale(Locale localeToSelect)
        {
            LocalizationSettings.SelectedLocale = localeToSelect;
        }
    }
}