#nullable enable
using Solar2048.Localization.UI;
using Solar2048.SaveLoad;
using Solar2048.StateMachine;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Solar2048.UI
{
    public sealed class MainMenuScreen : UIScreen, IMainMenuScreen
    {
        [SerializeField]
        private Button _continueButton = null!;

        [SerializeField]
        private Button _newGameButton = null!;

        [SerializeField]
        private Button _exitButton = null!;

        private IGameLifeCycle _gameLifeCycle = null!;
        private SaveController _saveController = null!;

        [Inject]
        private void Construct(IGameLifeCycle gameLifeCycle, SaveController saveController)
        {
            _saveController = saveController;
            _gameLifeCycle = gameLifeCycle;
        }

        private void Awake()
        {
            _newGameButton.onClick.AddListener(OnNewGame);
            _exitButton.onClick.AddListener(OnExit);
            _continueButton.onClick.AddListener(OnContinue);
        }

        protected override void OnShow()
        {
            bool isSaveAvailable = _saveController.IsSaveAvailable();
            _continueButton.gameObject.SetActive(isSaveAvailable);
        }

        protected override void OnHide()
        {
        }

        private void OnNewGame() => _gameLifeCycle.NewGame();
        private void OnExit() => _gameLifeCycle.ExitGame();
        private void OnContinue() => _gameLifeCycle.Load();
    }
}