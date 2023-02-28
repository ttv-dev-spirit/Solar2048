#nullable enable
using JetBrains.Annotations;
using Solar2048.Infrastructure;
using Solar2048.Input;
using Solar2048.StateMachine.Game.States;
using UniRx;
using UnityEngine.Assertions;

namespace Solar2048.StateMachine.Game
{
    // TODO (Stas): Extract state machine from GameLifeCycleController
    [UsedImplicitly]
    public sealed class GameStateMachine : IGameLifeCycle, IStateMachine
    {
        private readonly CompositeDisposable _subs = new();
        private readonly ReactiveProperty<State?> _currentState = new();

        private readonly IGameQuitter _gameQuitter;

        private readonly NewGameState _newGameState;
        private readonly GameRoundState _gameRoundState;
        private readonly DisposeResourcesState _disposeResourcesState;
        private readonly InitializeGameState _initializeGameState;
        private readonly MainMenuState _mainMenuState;

        public IReadOnlyReactiveProperty<State?> CurrentState => _currentState;

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
            _currentState.Value?.Exit();
            _disposeResourcesState.Enter();
        }

        public void ExitGame() => _gameQuitter.QuitGame();
        public void NewGame() => ChangeState(_newGameState);
        public void MainMenu() => ChangeState(_mainMenuState);

        public void Pause()
        {
            Assert.IsTrue(_currentState.Value == _gameRoundState);
            _gameRoundState.PauseGame();
        }

        public void Resume()
        {
            Assert.IsTrue(_currentState.Value == _gameRoundState);
            _gameRoundState.ResumeGame();
        }

        private void ChangeState(State state)
        {
            _currentState.Value?.Exit();
            _currentState.Value = state;
            _currentState.Value.Enter();
        }

        private void HandleInput(Unit _) => _currentState.Value?.HandleInput();
        private void OnInitializeFinished(State _) => ChangeState(_mainMenuState);
        private void OnNewGameStarted(State _) => ChangeState(_gameRoundState);

        private void OnResourcesDisposed(State _)
        {
            _subs.Clear();
            _currentState.Dispose();
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