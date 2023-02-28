#nullable enable
using System;
using JetBrains.Annotations;
using Solar2048.StateMachine.States;
using UniRx;

namespace Solar2048.StateMachine
{
    // TODO (Stas): Extract state machine from GameLifeCycleController
    [UsedImplicitly]
    public sealed class GameStateMachine : IGameLifeCycle, IStateMachine
    {
        private readonly CompositeDisposable _subs = new();

        private readonly Subject<State> _onStateChanged = new();
        private readonly NewGameState _newGameState;
        private readonly GameRoundState _gameRoundState;
        private readonly DisposeResourcesState _disposeResourcesState;
        private readonly InitializeGameState _initializeGameState;
        private readonly MainMenuState _mainMenuState;
        private readonly IGameQuitter _gameQuitter;

        private State? _currentState;

        public IObservable<State> OnStateChanged => _onStateChanged;

        public GameStateMachine(
            InputSystem inputSystem,
            GameStateFactory gameStateFactory,
            IGameQuitter gameQuitter)
        {
            _gameQuitter = gameQuitter;
            _newGameState = gameStateFactory.NewGameState;
            _gameRoundState = gameStateFactory.GameRoundState;
            _disposeResourcesState = gameStateFactory.DisposeResourcesState;
            _initializeGameState = gameStateFactory.InitializeGameState;
            _mainMenuState = gameStateFactory.MainMenuState;
            inputSystem.OnHandleInput.Subscribe(HandleInput).AddTo(_subs);
            SubscribeToEvents();
        }

        public void Initialize() => ChangeState(_initializeGameState);

        public void Dispose()
        {
            _currentState?.Exit();
            _disposeResourcesState.Enter();
        }

        public void ExitGame() => _gameQuitter.QuitGame();
        public void NewGame() => ChangeState(_newGameState);

        private void ChangeState(State state)
        {
            _currentState?.Exit();
            _currentState = state;
            _currentState.Enter();
            _onStateChanged.OnNext(_currentState);
        }

        private void HandleInput(Unit _) => _currentState?.HandleInput();
        private void OnInitializeFinished(State _) => ChangeState(_mainMenuState);
        private void OnNewGameStarted(State _) => ChangeState(_gameRoundState);

        private void OnResourcesDisposed(State _)
        {
            _subs.Clear();
            _onStateChanged.Dispose();
        }

        // TODO (Stas): Better way to finish states needed
        private void SubscribeToEvents()
        {
            _initializeGameState.OnStateFinished.Subscribe(OnInitializeFinished).AddTo(_subs);
            _newGameState.OnStateFinished.Subscribe(OnNewGameStarted).AddTo(_subs);
            _disposeResourcesState.OnStateFinished.Subscribe(OnResourcesDisposed).AddTo(_subs);
        }
    }
}