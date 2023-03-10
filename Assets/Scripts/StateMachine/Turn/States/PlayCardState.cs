#nullable enable
using System;
using Solar2048.Cards;
using UniRx;
using Zenject;

namespace Solar2048.StateMachine.Turn.States
{
    public sealed class PlayCardState : State
    {
        private readonly CardPlayer _cardPlayer;
        private IDisposable _sub = null!;

        public PlayCardState(CardPlayer cardPlayer)
        {
            _cardPlayer = cardPlayer;
        }

        protected override void OnEnter()
        {
            _sub = _cardPlayer.OnCardPlayed.Subscribe(CardPlayedHandler);
            _cardPlayer.Activate();
        }

        protected override void OnExit()
        {
            _cardPlayer.Deactivate();
            _sub.Dispose();
        }

        private void CardPlayedHandler(Unit _)
        {
            Finish();
        }

        public class Factory : PlaceholderFactory<PlayCardState>
        {
        }
    }
}