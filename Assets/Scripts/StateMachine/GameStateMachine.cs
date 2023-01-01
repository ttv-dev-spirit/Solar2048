#nullable enable
using System;
using Solar2048.Buildings;
using UniRx;

namespace Solar2048.StateMachine
{
    public sealed class GameStateMachine
    {
        private readonly Subject<State> _onStateChanged = new();
        private readonly InitializeGameState _initializeGameState;
        private readonly RoundState _roundState;

        private State? _currentState;

        public IObservable<State> OnStateChanged => _onStateChanged;
        public State? CurrentState => _currentState;

        public GameStateMachine(BuildingsManager buildingsManager, CardSpawner cardSpawner, MessageBroker messageBroker)
        {
            _roundState = new RoundState(buildingsManager, cardSpawner);
            _initializeGameState = new InitializeGameState(this, messageBroker);
        }

        public void Initialize() => ChangeState(_initializeGameState);
        public void Round() => ChangeState(_roundState);

        private void ChangeState(State state)
        {
            _currentState?.Exit();
            _currentState = state;
            _currentState.Enter();
            _onStateChanged.OnNext(_currentState);
        }

        private void HandleInput(Unit _)
        {
            _currentState?.HandleInput();
        }
    }
}