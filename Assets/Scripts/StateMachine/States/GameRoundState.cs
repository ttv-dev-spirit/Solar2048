#nullable enable
using System;
using Solar2048.Buildings;
using Solar2048.Cards;
using Solar2048.Map;
using UniRx;
using Zenject;

namespace Solar2048.StateMachine.States
{
    public sealed class GameRoundState : State
    {
        private const int NUMBER_OF_CARD_PLAYS_BEFORE_MOVE = 3;

        private readonly ReactiveProperty<State?> _currentState = new();
        private readonly ReactiveProperty<int> _cardsPlayedCounter = new();
        private readonly CardSpawner _cardSpawner;
        private readonly State _playCardState;
        private readonly BotMoveState _moveState;
        private readonly CompositeDisposable _subs = new();

        public IReadOnlyReactiveProperty<State?> CurrentState => _currentState;
        public IReadOnlyReactiveProperty<int> CardsPlayedCounter => _cardsPlayedCounter;
        public MoveDirection NextDirection => _moveState.NextDirection;

        public GameRoundState(CardSpawner cardSpawner, TurnStateFactory turnStateFactory)
        {
            _cardSpawner = cardSpawner;
            _moveState = turnStateFactory.BotMoveState;
            _playCardState = turnStateFactory.PlayCardState;
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
            _moveState.RollNextDirection();

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
            _moveState.OnStateExit.Subscribe(OnMoveStateExitHandler).AddTo(_subs);
            _playCardState.OnStateExit.Subscribe(OnPlayCardStateExitHandler).AddTo(_subs);
            _cardsPlayedCounter.Value = 0;
            _moveState.RollNextDirection();

            EnterState(_playCardState);
        }

        protected override void OnExit()
        {
            _subs.Clear();
        }

        private void EnterState(State state)
        {
            _currentState.Value = state;
            state.Enter();
        }

        public class Factory : PlaceholderFactory<GameRoundState>
        {
        }
    }
}