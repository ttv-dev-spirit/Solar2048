#nullable enable
using Solar2048.StateMachine.Messages;
using Solar2048.UI;
using UniRx;
using Zenject;

namespace Solar2048.StateMachine.States
{
    public sealed class InitializeGameState : State
    {
        private readonly IMessagePublisher _messagePublisher;
        private readonly UIManager _uiManager;

        public InitializeGameState(IMessagePublisher messagePublisher,
            UIManager uiManager)
        {
            _messagePublisher = messagePublisher;
            _uiManager = uiManager;
        }

        protected override void OnEnter()
        {
            _uiManager.HideAll();
            _messagePublisher.Publish(new NewGameMessage());
            Exit();
        }

        protected override void OnExit()
        {
        }

        public class Factory : PlaceholderFactory<InitializeGameState>
        {
        }
    }
}