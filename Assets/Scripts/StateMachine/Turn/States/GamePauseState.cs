#nullable enable
using Solar2048.UI;
using Zenject;

namespace Solar2048.StateMachine.Turn.States
{
    public sealed class GamePauseState : State
    {
        private readonly UIManager _uiManager;

        private GamePauseScreen _gamePauseScreen = null!;

        public GamePauseState(UIManager uiManager)
        {
            _uiManager = uiManager;
        }

        protected override void OnEnter()
        {
            _gamePauseScreen = _uiManager.GetScreen<GamePauseScreen>();
            _gamePauseScreen.Show();
        }

        protected override void OnExit()
        {
            _gamePauseScreen.Hide();
        }

        public class Factory : PlaceholderFactory<GamePauseState>
        {
        }
    }
}