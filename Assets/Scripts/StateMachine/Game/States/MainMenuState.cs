#nullable enable

using Solar2048.UI;
using Zenject;

namespace Solar2048.StateMachine.Game.States
{
    public sealed class MainMenuState : State
    {
        private readonly UIManager _uiManager;

        private MainMenuScreen _mainMenu = null!;

        public MainMenuState(UIManager uiManager)
        {
            _uiManager = uiManager;
        }

        protected override void OnEnter()
        {
            _mainMenu = _uiManager.GetScreen<MainMenuScreen>();
            _mainMenu.Show();
        }

        protected override void OnExit()
        {
            _mainMenu.Hide();
        }

        public class Factory : PlaceholderFactory<MainMenuState>
        {
        }
    }
}