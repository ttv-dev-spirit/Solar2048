#nullable enable
using System.Collections.Generic;
using UnityEngine;

namespace Solar2048.Buildings
{
    public sealed class BuildingsManager
    {
        private readonly BuildingsFactory _buildingsFactory;
        private readonly GameField _gameField;
        
        private readonly List<Building> _buildings = new();

        public BuildingsManager(BuildingsFactory buildingsFactory, GameField gameField)
        {
            _buildingsFactory = buildingsFactory;
            _gameField = gameField;
        }

        public void AddNewBuildingTo(BuildingType buildingType, Vector2Int position)
        {
            Building building = _buildingsFactory.Create(buildingType);
            _buildings.Add(building);
            building.SetPosition(position);
            _gameField.RegisterBuilding(building, position);
        }
    }
}