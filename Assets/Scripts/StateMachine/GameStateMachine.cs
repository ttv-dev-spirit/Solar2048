#nullable enable
using System;
using JetBrains.Annotations;
using Solar2048.StateMachine.States;
using UniRx;

namespace Solar2048.StateMachine
{
    [UsedImplicitly]
    public sealed class GameStateMachine
    {
        private readonly InitializeGameState _initializeGameState;
        private readonly GameRoundState _gameRoundState;
        private readonly Subject<State> _onStateChanged = new();

        private State? _currentState;

        public IObservable<State> OnStateChanged => _onStateChanged;

        public GameStateMachine(
            InputSystem inputSystem,
            GameStateFactory gameStateFactory)
        {
            _initializeGameState = gameStateFactory.InitializeGameState;
            _gameRoundState = gameStateFactory.GameRoundState;
            inputSystem.OnHandleInput.Subscribe(HandleInput);
            _initializeGameState.OnStateExit.Subscribe(OnInitializeFinished);
        }

        public void Initialize() => ChangeState(_initializeGameState);

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

        private void OnInitializeFinished(State _)
        {
            // HACK (Stas): This is stupid, used to prevent infinite cycle InitializeGameState.Exit => OnInitializedFinished() => ChangeState() => _currentState?.Exit ...I need a better way for state to exit once.
            _currentState = null;
            ChangeState(_gameRoundState);
        }
    }
}