#nullable enable
using System;
using JetBrains.Annotations;
using Solar2048.StateMachine.States;
using UniRx;

namespace Solar2048.StateMachine
{
    [UsedImplicitly]
    public sealed class GameStateMachine : IDisposable
    {
        private readonly CompositeDisposable _subs = new();

        private readonly NewGameState _newGameState;
        private readonly GameRoundState _gameRoundState;
        private readonly DisposeResourcesState _disposeResourcesState;
        private readonly InitializeGameState _initializeGameState;
        private readonly Subject<State> _onStateChanged = new();

        private State? _currentState;

        public IObservable<State> OnStateChanged => _onStateChanged;

        public GameStateMachine(
            InputSystem inputSystem,
            GameStateFactory gameStateFactory)
        {
            _newGameState = gameStateFactory.NewGameState;
            _gameRoundState = gameStateFactory.GameRoundState;
            _disposeResourcesState = gameStateFactory.DisposeResourcesState;
            // _initializeGameState = gameStateFactory.InitializeGameState;
            inputSystem.OnHandleInput.Subscribe(HandleInput).AddTo(_subs);
            _newGameState.OnStateFinished.Subscribe(OnInitializeFinished).AddTo(_subs);
            _disposeResourcesState.OnStateFinished.Subscribe(OnResourcesDisposed).AddTo(_subs);
        }

        public void Initialize() => ChangeState(_newGameState);

        public void Dispose()
        {
            _currentState?.Exit();
            _currentState = _disposeResourcesState;
            _currentState.Enter();
        }

        private void ChangeState(State state)
        {
            _currentState?.Exit();
            _currentState = state;
            _currentState.Enter();
            _onStateChanged.OnNext(_currentState);
        }

        private void HandleInput(Unit _) => _currentState?.HandleInput();
        private void OnInitializeFinished(State _) => ChangeState(_gameRoundState);

        private void OnResourcesDisposed(State _)
        {
            _subs.Clear();
            _onStateChanged.Dispose();
        }
    }
}