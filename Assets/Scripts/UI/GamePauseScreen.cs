#nullable enable
using Solar2048.StateMachine;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Solar2048.UI
{
    public sealed class GamePauseScreen : UIScreen
    {
        private IGameLifeCycle _gameLifeCycle = null!;

        [SerializeField]
        private Button _resumeButton = null!;

        [SerializeField]
        private Button _restartButton = null!;

        [SerializeField]
        private Button _toMainMenuButton = null!;

        [Inject]
        private void Construct(IGameLifeCycle gameLifeCycle)
        {
            _gameLifeCycle = gameLifeCycle;
        }

        private void Awake()
        {
            _resumeButton.onClick.AddListener(OnResume);
            _restartButton.onClick.AddListener(OnRestart);
            _toMainMenuButton.onClick.AddListener(OnMainMenu);
        }

        protected override void OnShow()
        {
        }

        protected override void OnHide()
        {
        }

        private void OnResume() => _gameLifeCycle.Resume();
        private void OnRestart() => _gameLifeCycle.NewGame();
        private void OnMainMenu() => _gameLifeCycle.MainMenu();
    }
}