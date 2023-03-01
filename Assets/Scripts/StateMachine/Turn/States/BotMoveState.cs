#nullable enable
using Solar2048.Map;
using Zenject;

namespace Solar2048.StateMachine.Turn.States
{
    public sealed class BotMoveState : State
    {
        private readonly BuildingMover _buildingMover;
        private readonly DirectionRoller _directionRoller;

        private bool _wasLoaded;

        public MoveDirection NextDirection { get; private set; }

        public BotMoveState(BuildingMover buildingMover, DirectionRoller directionRoller)
        {
            _buildingMover = buildingMover;
            _directionRoller = directionRoller;
        }

        public void RollNextDirection()
        {
            // TODO (Stas): I think this should be handled somewhere up the chain.
            if (_wasLoaded)
            {
                _wasLoaded = false;
                return;
            }

            NextDirection = _directionRoller.Roll();
        }

        public void LoadNextDirection(MoveDirection nextDirection)
        {
            NextDirection = nextDirection;
            _wasLoaded = true;
        }

        protected override void OnEnter()
        {
            _buildingMover.Activate();
            _buildingMover.MoveBuildings(NextDirection);
            Finish();
        }

        protected override void OnExit()
        {
            _buildingMover.Deactivate();
        }

        public class Factory : PlaceholderFactory<BotMoveState>
        {
        }
    }
}