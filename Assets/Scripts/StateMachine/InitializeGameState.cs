#nullable enable
using UniRx;

namespace Solar2048.StateMachine
{
    public sealed class InitializeGameState : State
    {
        private readonly MessageBroker _messageBroker;
        private readonly GameStateMachine _gameStateMachine;

        public InitializeGameState(GameStateMachine gameStateMachine, MessageBroker messageBroker)
        {
            _messageBroker = messageBroker;
            _gameStateMachine = gameStateMachine;
        }

        protected override void OnEnter()
        {
            _messageBroker.Publish(new NewGameMessage());
            _gameStateMachine.Round();
        }

        protected override void OnExit()
        {
        }
    }
}