#nullable enable
using Solar2048.Buildings;

namespace Solar2048.StateMachine
{
    public sealed class RoundState : State
    {
        private readonly BuildingsManager _buildingsManager;

        public RoundState(BuildingsManager buildingsManager)
        {
            _buildingsManager = buildingsManager;
        }

        protected override void OnEnter()
        {
            _buildingsManager.AddNewBuildingTo(BuildingType.WindTurbine, 0, 0);
        }

        protected override void OnExit()
        {
        }
    }
}