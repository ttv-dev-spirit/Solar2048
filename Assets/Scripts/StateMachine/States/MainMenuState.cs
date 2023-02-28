#nullable enable

using Solar2048.Localization.UI;
using Solar2048.UI;

namespace Solar2048.StateMachine.States
{
    public sealed class MainMenuState : State
    {
        private readonly UIManager _uiManager;

        private IMainMenuScreen _mainMenu;

        public MainMenuState(UIManager uiManager)
        {
            _uiManager = uiManager;
        }

        protected override void OnEnter()
        {
            _mainMenu = _uiManager.GetScreen<MainMainMenuScreen>();
            _mainMenu.Show();
        }

        protected override void OnExit()
        {
            _mainMenu.Hide();
        }
    }
}