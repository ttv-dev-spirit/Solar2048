#nullable enable
using Solar2048.Buildings;
using UnityEngine;

namespace Solar2048.StateMachine
{
    public sealed class RoundState : State
    {
        private readonly BuildingsManager _buildingsManager;
        private readonly CardSpawner _cardSpawner;

        public RoundState(BuildingsManager buildingsManager, CardSpawner cardSpawner)
        {
            _buildingsManager = buildingsManager;
            _cardSpawner = cardSpawner;
        }

        protected override void OnEnter()
        {
            _buildingsManager.AddNewBuildingTo(BuildingType.WindTurbine, new Vector2Int(3, 3));
         _cardSpawner.AddCardToHand(BuildingType.WindTurbine);   
         _cardSpawner.AddCardToHand(BuildingType.WindTurbine);   
        }

        protected override void OnExit()
        {
        }
    }
}