#nullable enable
using System;
using Solar2048.StateMachine.States;

namespace Solar2048.StateMachine
{
    public interface IStateMachine
    {
        IObservable<State> OnStateChanged { get; }
    }
}