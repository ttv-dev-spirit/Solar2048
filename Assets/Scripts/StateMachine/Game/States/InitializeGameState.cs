#nullable enable
using Solar2048.Cheats;
using Solar2048.StaticData;
using Solar2048.UI;
using Zenject;

namespace Solar2048.StateMachine.Game.States
{
    public sealed class InitializeGameState : State
    {
        private readonly UIManager _uiManager;
        private readonly CheatsContainer _cheatsContainer;

        public InitializeGameState(UIManager uiManager, CheatsContainer cheatsContainer)
        {
            _cheatsContainer = cheatsContainer;
            _uiManager = uiManager;
        }

        protected override void OnEnter()
        {
            _cheatsContainer.Activate();
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