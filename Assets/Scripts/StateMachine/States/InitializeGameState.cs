#nullable enable
using Solar2048.UI;
using Zenject;

namespace Solar2048.StateMachine.States
{
    public sealed class InitializeGameState : State
    {
        private readonly UIManager _uiManager;

        public InitializeGameState(UIManager uiManager)
        {
            _uiManager = uiManager;
        }

        protected override void OnEnter()
        {
            _uiManager.HideAll();
            Finish();
        }

        protected override void OnExit()
        {
        }

        public class Factory : PlaceholderFactory<InitializeGameState>
        {
        }
    }
}