#nullable enable
using Solar2048.Buildings;
using UniRx;

namespace Solar2048.StateMachine
{
    public sealed class GameRoundState : State
    {
        private const int NUMBER_OF_CARD_PLAYS_BEFORE_MOVE = 3;

        private readonly ReactiveProperty<State?> _currentState = new();
        private readonly ReactiveProperty<int> _cardsPlayedCounter = new();
        private readonly CardSpawner _cardSpawner;
        private readonly State _playCardState;
        private readonly State _moveState;

        public IReadOnlyReactiveProperty<State?> CurrentState => _currentState;
        public IReadOnlyReactiveProperty<int> CardsPlayedCounter => _cardsPlayedCounter;

        public GameRoundState(CardSpawner cardSpawner, CardPlayer cardPlayer, BuildingMover buildingMover)
        {
            _cardSpawner = cardSpawner;
            _moveState = new MoveState(buildingMover);
            _playCardState = new PlayCardState(cardPlayer);
            _moveState.OnStateExit.Subscribe(OnMoveStateExitHandler);
            _playCardState.OnStateExit.Subscribe(OnPlayCardStateExitHandler);
            Test();
        }

        private void Test()
        {
            _cardSpawner.AddCardToHand(BuildingType.WindTurbine);
            _cardSpawner.AddCardToHand(BuildingType.WindTurbine);
            _cardSpawner.AddCardToHand(BuildingType.WindTurbine);
            _cardSpawner.AddCardToHand(BuildingType.WindTurbine);
        }

        private void OnMoveStateExitHandler(State _)
        {
            _cardsPlayedCounter.Value = 0;
            EnterState(_playCardState);
        }

        private void OnPlayCardStateExitHandler(State _)
        {
            _cardsPlayedCounter.Value++;
            if (_cardsPlayedCounter.Value >= NUMBER_OF_CARD_PLAYS_BEFORE_MOVE)
            {
                EnterState(_moveState);
                return;
            }

            EnterState(_playCardState);
        }

        protected override void OnEnter()
        {
            _cardsPlayedCounter.Value = 0;
            EnterState(_playCardState);
        }

        protected override void OnExit()
        {
        }

        private void EnterState(State state)
        {
            _currentState.Value = state;
            state.Enter();
        }
    }
}