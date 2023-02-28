#nullable enable
using Solar2048.StateMachine;
using Solar2048.StateMachine.Game.States;
using Solar2048.StateMachine.Turn.States;
using Solar2048.UI.Skins;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;
using Zenject;

namespace Solar2048.UI
{
    public sealed class RoundStatePanel : MonoBehaviour
    {
        private readonly CompositeDisposable _sub = new();

        private GameRoundState? _gameRoundState;

        [SerializeField]
        private GameObject _panel = null!;

        [SerializeField]
        private ActivatableSkinController[] _cardPlayCounters = null!;

        [SerializeField]
        private NextMovePanel _movePanel = null!;

        [Inject]
        private void Construct(IStateMachine stateMachine)
        {
            stateMachine.CurrentState.Subscribe(StateChangedHandler);
        }

        private void StateChangedHandler(State? state)
        {
            if (state is GameRoundState gameRoundState)
            {
                _gameRoundState = gameRoundState;
                Show();
                return;
            }

            Hide();
        }

        private void Show()
        {
            Assert.IsNotNull(_gameRoundState);
            _panel.SetActive(true);
            _gameRoundState!.CurrentState.Subscribe(_ => UpdatePanel()).AddTo(_sub);
            _gameRoundState.CardsPlayedCounter.Subscribe(_ => UpdatePanel()).AddTo(_sub);
        }

        private void Hide()
        {
            _sub.Clear();
            _panel.SetActive(false);
        }

        private void UpdatePanel()
        {
            Assert.IsNotNull(_gameRoundState);
            _movePanel.SetDirection(_gameRoundState!.NextDirection);
            if (_gameRoundState!.CurrentState.Value is MoveState)
            {
                ShowMoveState();
                return;
            }

            ShowCardPlayState();
        }

        private void ShowMoveState()
        {
            DeactivateAllCardPlayCounters();
            _movePanel.Activate();
        }

        private void ShowCardPlayState()
        {
            for (var i = 0; i < _cardPlayCounters.Length; i++)
            {
                if (i == _gameRoundState!.CardsPlayedCounter.Value)
                {
                    _cardPlayCounters[i].Activate();
                }
                else
                {
                    _cardPlayCounters[i].Deactivate();
                }
            }

            _movePanel.Deactivate();
        }

        private void DeactivateAllCardPlayCounters()
        {
            foreach (ActivatableSkinController cardPlayCounter in _cardPlayCounters)
            {
                cardPlayCounter.Deactivate();
            }
        }
    }
}