#nullable enable
using Solar2048.Map;

namespace Solar2048.StateMachine.States
{
    public sealed class BotMoveState : State
    {
        private readonly BuildingMover _buildingMover;
        private readonly DirectionRoller _directionRoller;
        
        public MoveDirection NextDirection { get; private set; }

        public BotMoveState(BuildingMover buildingMover, DirectionRoller directionRoller)
        {
            _buildingMover = buildingMover;
            _directionRoller = directionRoller;
        }

        public void RollNextDirection()
        {
            NextDirection = _directionRoller.Roll();
        }

        protected override void OnEnter()
        {
            _buildingMover.Activate();
            _buildingMover.MoveBuildings(NextDirection);
            Exit();
        }

        protected override void OnExit()
        {
            _buildingMover.Deactivate();
        }
    }
}