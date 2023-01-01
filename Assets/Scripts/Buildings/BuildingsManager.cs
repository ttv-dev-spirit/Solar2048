#nullable enable
using System.Collections.Generic;
using UnityEngine;

namespace Solar2048.Buildings
{
    public sealed class BuildingsManager
    {
        private readonly List<Building> _buildings = new();
        private readonly BuildingsFactory _buildingsFactory;

        public BuildingsManager(BuildingsFactory buildingsFactory)
        {
            _buildingsFactory = buildingsFactory;
        }

        public void AddNewBuildingTo(BuildingType buildingType, Vector2Int position)
        {
            Building building = _buildingsFactory.Create(buildingType);
            _buildings.Add(building);
            building.SetPosition(position);
        }
    }
}