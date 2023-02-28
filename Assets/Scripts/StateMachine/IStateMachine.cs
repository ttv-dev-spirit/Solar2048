#nullable enable
using UniRx;

namespace Solar2048.StateMachine
{
    public interface IStateMachine
    {
        IReadOnlyReactiveProperty<State?> CurrentState { get; }
    }
}