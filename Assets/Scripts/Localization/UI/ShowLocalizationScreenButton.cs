#nullable enable
using System;
using Solar2048.UI;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Solar2048.Localization.UI
{
    public sealed class ShowLocalizationScreenButton : MonoBehaviour
    {
        private UIManager _uiManager = null!;

        [SerializeField]
        private Button _button = null!;

        [Inject]
        private void Construct(UIManager uiManager, LocalizationController localizationController)
        {
            _uiManager = uiManager;
            localizationController.IsInitialized.Subscribe(UpdateButtonStatus);
        }

        private void Awake()
        {
            _button.onClick.AddListener(OnClickedHandler);
        }

        private void OnClickedHandler()
        {
            _uiManager.GetScreen<LocalizationScreen>().Show();
        }

        private void UpdateButtonStatus(bool isActive)
        {
            _button.interactable = isActive;
        }
    }
}