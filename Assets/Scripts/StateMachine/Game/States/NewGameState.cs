#nullable enable
using Solar2048.Cheats;
using Zenject;

namespace Solar2048.StateMachine.Game.States
{
    public sealed class NewGameState : State
    {
        private IGameStateReseter _gameStateReseter;
        private CheatsContainer _cheatsContainer;

        public NewGameState(IGameStateReseter gameStateReseter, CheatsContainer cheatsContainer)
        {
            _cheatsContainer = cheatsContainer;
            _gameStateReseter = gameStateReseter;
        }

        protected override void OnEnter()
        {
            _gameStateReseter.Reset();
            _cheatsContainer.Reset();
            Finish();
        }

        protected override void OnExit()
        {
        }


        public class Factory : PlaceholderFactory<NewGameState>
        {
        }
    }
}