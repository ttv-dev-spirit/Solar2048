#nullable enable
using Solar2048.UI;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Solar2048.Localization.UI
{
    public sealed class LocalizationScreen : UIScreen, ILocalizationScreen
    {
        private readonly CompositeDisposable _subs = new();

        private LocalizationController _localizationController = null!;

        [SerializeField]
        private ElementsList _buttonsList = null!;

        [SerializeField]
        private Button _closeButton = null!;

        [Inject]
        private void Construct(LocalizationController localizationController)
        {
            _localizationController = localizationController;
        }

        private void Awake()
        {
            _closeButton.onClick.AddListener(OnCloseClickedHandler);
        }

        protected override void OnShow()
        {
            var availableLocales = _localizationController.AvailableLanguages;
            var buttons = _buttonsList.GetElements<LocalizationButton>(availableLocales.Count);
            for (var i = 0; i < availableLocales.Count; i++)
            {
                buttons[i].Show(availableLocales[i]);
                buttons[i].OnClicked.Subscribe(OnButtonClicked).AddTo(_subs);
            }
        }

        protected override void OnHide()
        {
            _buttonsList.HideButtons();
            _subs.Clear();
        }

        private void OnButtonClicked(Language language) => _localizationController.SelectLanguage(language);
        private void OnCloseClickedHandler() => Hide();
    }
}