#nullable enable
using Solar2048.StateMachine;
using Solar2048.UI;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Solar2048.Localization.UI
{
    public sealed class MainMenuScreen : UIScreen, IMainMenuScreen
    {
        [SerializeField]
        private Button _newGameButton = null!;

        [SerializeField]
        private Button _exitButton = null!;

        private IGameLifeCycle _gameLifeCycle = null!;

        [Inject]
        private void Construct(IGameLifeCycle gameLifeCycle)
        {
            _gameLifeCycle = gameLifeCycle;
        }

        private void Awake()
        {
            _newGameButton.onClick.AddListener(OnNewGame);
            _exitButton.onClick.AddListener(OnExit);
        }

        protected override void OnShow()
        {
        }

        protected override void OnHide()
        {
        }

        private void OnNewGame() => _gameLifeCycle.NewGame();
        private void OnExit() => _gameLifeCycle.ExitGame();
    }
}