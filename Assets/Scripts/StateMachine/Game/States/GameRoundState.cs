#nullable enable
using Solar2048.AssetManagement;
using Solar2048.Cycles;
using Solar2048.Map;
using Solar2048.SaveLoad;
using Solar2048.StateMachine.Turn;
using Solar2048.StateMachine.Turn.States;
using UniRx;
using UnityEngine.Assertions;
using Zenject;

namespace Solar2048.StateMachine.Game.States
{
    public sealed class GameRoundState : State, IRoundLifeCycle, IStateMachine, ISavable, ILoadable
    {
        private const int NUMBER_OF_CARD_PLAYS_BEFORE_MOVE = 3;

        private readonly ReactiveProperty<State?> _currentState = new();
        private readonly ReactiveProperty<int> _cardsPlayedCounter = new();
        private readonly State _playCardState;
        private readonly BotMoveState _moveState;
        private readonly GamePauseState _gamePauseState;
        private readonly ConfirmTurnState _confirmTurnState;
        private readonly SaveController _saveController;
        private readonly CompositeDisposable _subs = new();

        private State? _stateBeforePause;
        private ICycleCounter _cycleCounter;

        public IReadOnlyReactiveProperty<State?> CurrentState => _currentState;
        public IReadOnlyReactiveProperty<int> CardsPlayedCounter => _cardsPlayedCounter;
        public MoveDirection NextDirection => _moveState.NextDirection;

        public GameRoundState(TurnStateFactory turnStateFactory, SaveController saveController,
            ICycleCounter cycleCounter)
        {
            _cycleCounter = cycleCounter;
            _saveController = saveController;
            _moveState = turnStateFactory.BotMoveState;
            _playCardState = turnStateFactory.PlayCardState;
            _gamePauseState = turnStateFactory.GamePauseState;
            _confirmTurnState = turnStateFactory.ConfirmTurnState;
            _saveController.Register(this);
        }

        protected override void OnEnter()
        {
            SubscribeToStateEvents();
            _cardsPlayedCounter.Value = 0;
            _moveState.RollNextDirection();
            _saveController.SaveGame();

            ChangeState(_playCardState);
        }

        protected override void OnExit()
        {
            _currentState.Value?.Exit();
            _subs.Clear();
        }

        public void PauseGame()
        {
            _stateBeforePause = _currentState.Value;
            ChangeState(_gamePauseState);
        }

        public void ResumeGame()
        {
            Assert.IsNotNull(_stateBeforePause);
            ChangeState(_stateBeforePause!);
            _stateBeforePause = null;
        }

        public void Save(GameData gameData)
        {
            gameData.NextDirection = NextDirection;
        }

        public void Load(GameData gameData)
        {
            _moveState.LoadNextDirection(gameData.NextDirection);
        }

        private void SubscribeToStateEvents()
        {
            _moveState.OnStateFinished.Subscribe(OnMoveStateFinished).AddTo(_subs);
            _playCardState.OnStateFinished.Subscribe(OnPlayStateFinished).AddTo(_subs);
            _confirmTurnState.OnStateFinished.Subscribe(OnConfirmTurnStateFinished).AddTo(_subs);
        }

        private void OnMoveStateFinished(State _)
        {
            _cardsPlayedCounter.Value = 0;
            _moveState.RollNextDirection();
            _cycleCounter.NextCycle();
            _saveController.SaveGame();
            ChangeState(_playCardState);
        }

        private void OnPlayStateFinished(State _)
        {
            _cardsPlayedCounter.Value++;
            if (_cardsPlayedCounter.Value >= NUMBER_OF_CARD_PLAYS_BEFORE_MOVE)
            {
                ChangeState(_confirmTurnState);
                return;
            }

            ChangeState(_playCardState);
        }

        private void OnConfirmTurnStateFinished(State _)
        {
            ChangeState(_moveState);
        }

        private void ChangeState(State state)
        {
            _currentState.Value?.Exit();
            _currentState.Value = state;
            state.Enter();
        }

        public class Factory : PlaceholderFactory<GameRoundState>
        {
        }
    }
}