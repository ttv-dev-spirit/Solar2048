#nullable enable
using System;
using Solar2048.UI;
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
        private void Construct(UIManager uiManager)
        {
            _uiManager = uiManager;
        }

        private void Awake()
        {
            _button.onClick.AddListener(OnClickedHandler);
        }

        private void OnClickedHandler()
        {
            _uiManager.GetScreen<ILocalizationScreen>().Show();
        }
    }
}