#nullable enable
using System;
using Solar2048.Map;
using UniRx;
using Zenject;

namespace Solar2048.StateMachine.States
{
    public sealed class MoveState : State
    {
        private readonly BuildingMover _buildingMover;
        private IDisposable _sub;


        public MoveState(BuildingMover buildingMover)
        {
            _buildingMover = buildingMover;
        }

        protected override void OnEnter()
        {
            _sub = _buildingMover.OnMoved.Subscribe(BuildingMovedHandler);
            _buildingMover.Activate();
        }

        protected override void OnExit()
        {
            _buildingMover.Deactivate();
            _sub.Dispose();
        }

        private void BuildingMovedHandler(Unit _)
        {
            Exit();
        }

        public class Factory : PlaceholderFactory<MoveState>
        {
        }
    }
}