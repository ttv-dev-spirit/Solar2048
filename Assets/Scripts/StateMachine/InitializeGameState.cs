#nullable enable
using UniRx;

namespace Solar2048.StateMachine
{
    public sealed class InitializeGameState : State
    {
        private readonly IMessagePublisher _messagePublisher;
        private readonly GameStateMachine _gameStateMachine;

        public InitializeGameState(GameStateMachine gameStateMachine, IMessagePublisher messagePublisher)
        {
            _messagePublisher = messagePublisher;
            _gameStateMachine = gameStateMachine;
        }

        protected override void OnEnter()
        {
            _messagePublisher.Publish(new NewGameMessage());
            _gameStateMachine.Round();
        }

        protected override void OnExit()
        {
        }
    }
}