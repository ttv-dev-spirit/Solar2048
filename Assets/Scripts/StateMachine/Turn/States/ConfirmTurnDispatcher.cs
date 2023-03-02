#nullable enable
using UniRx;

namespace Solar2048.StateMachine.Turn.States
{
    public sealed class ConfirmTurnDispatcher
    {
        public readonly ReactiveProperty<bool> IsConfirmState = new();
        public readonly Subject<Unit> OnConfirm = new();
    }
}