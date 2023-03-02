#nullable enable
using Solar2048.UI;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Solar2048.StateMachine.Turn.States
{
    public sealed class ConfirmTurnPanel : UIScreen
    {
        [SerializeField]
        private Button _confirmButton = null!;

        private ConfirmTurnDispatcher _confirmTurnDispatcher = null!;

        [Inject]
        private void Construct(ConfirmTurnDispatcher confirmTurnDispatcher)
        {
            _confirmTurnDispatcher = confirmTurnDispatcher;
            _confirmTurnDispatcher.IsConfirmState.Subscribe(ButtonUpdate);
        }

        private void Awake()
        {
            _confirmButton.onClick.AddListener(OnConfirm);
        }

        protected override void OnShow()
        {
        }

        protected override void OnHide()
        {
        }

        private void OnConfirm() => _confirmTurnDispatcher.OnConfirm.OnNext(Unit.Default);
        private void ButtonUpdate(bool isConfirmTurn) => _confirmButton.interactable = isConfirmTurn;
    }
}