#nullable enable
using UniRx;
using Zenject;

namespace Solar2048.StateMachine.Turn.States
{
    public sealed class ConfirmTurnState : State
    {
        private readonly ConfirmTurnDispatcher _confirmTurnDispatcher;

        public ConfirmTurnState(ConfirmTurnDispatcher confirmTurnDispatcher)
        {
            _confirmTurnDispatcher = confirmTurnDispatcher;
            _confirmTurnDispatcher.OnConfirm.Subscribe(OnConfirm);
        }

        protected override void OnEnter()
        {
            _confirmTurnDispatcher.IsConfirmState.Value = true;
        }

        protected override void OnExit()
        {
            _confirmTurnDispatcher.IsConfirmState.Value = false;
        }

        private void OnConfirm(Unit _) => Finish();

        public class Factory : PlaceholderFactory<ConfirmTurnState>
        {
        }
    }
}