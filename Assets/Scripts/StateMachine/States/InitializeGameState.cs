#nullable enable
using Solar2048.Buildings;
using Solar2048.StateMachine.Messages;
using Solar2048.UI;
using UniRx;

namespace Solar2048.StateMachine.States
{
    public sealed class InitializeGameState : State
    {
        private readonly IMessagePublisher _messagePublisher;
        private readonly GameStateMachine _gameStateMachine;
        private readonly UIManager _uiManager;

        public InitializeGameState(GameStateMachine gameStateMachine, IMessagePublisher messagePublisher,
            UIManager uiManager)
        {
            _messagePublisher = messagePublisher;
            _gameStateMachine = gameStateMachine;
            _uiManager = uiManager;
        }

        protected override void OnEnter()
        {
            _uiManager.HideAll();
            _messagePublisher.Publish(new NewGameMessage());
            _gameStateMachine.Round();
        }

        protected override void OnExit()
        {
        }
    }
}