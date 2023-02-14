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
        private readonly GameRoundState _gameRoundState;

        private State? _currentState;

        public IObservable<State> OnStateChanged => _onStateChanged;
        public State? CurrentState => _currentState;

        public GameStateMachine(CardSpawner cardSpawner, BuildingMover buildingMover,
            InputSystem inputSystem,
            IMessagePublisher messagePublisher,
            UIManager uiManager,
            CardPlayer cardPlayer)
        {
            _gameRoundState = new GameRoundState(cardSpawner, cardPlayer, buildingMover);
            _gameRoundState.AddInputHandler(new BuildingsMoveHandler(buildingMover));
            _initializeGameState = new InitializeGameState(this, messagePublisher, uiManager);
            inputSystem.OnHandleInput.Subscribe(HandleInput);
        }

        public void Initialize() => ChangeState(_initializeGameState);
        public void Round() => ChangeState(_gameRoundState);

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