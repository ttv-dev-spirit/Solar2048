#nullable enable
using Solar2048.Buildings;
using UnityEngine;

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
            _buildingsManager.AddNewBuildingTo(BuildingType.WindTurbine, new Vector2Int(3, 3));
        }

        protected override void OnExit()
        {
        }
    }
}