#nullable enable
using System;
using UniRx;

namespace Solar2048.StateMachine
{
    public sealed class PlayCardState : State
    {
        private readonly CardPlayer _cardPlayer;
        private IDisposable _sub;

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
            Exit();
        }
    }
}