#nullable enable
using Solar2048.StateMachine.Messages;
using Solar2048.UI;
using UniRx;
using Zenject;

namespace Solar2048.StateMachine.States
{
    public sealed class NewGameState : State
    {
        private readonly IMessagePublisher _messagePublisher;
        private readonly UIManager _uiManager;

        public NewGameState(IMessagePublisher messagePublisher,
            UIManager uiManager)
        {
            _messagePublisher = messagePublisher;
            _uiManager = uiManager;
        }

        protected override void OnEnter()
        {
            _messagePublisher.Publish(new NewGameMessage());
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