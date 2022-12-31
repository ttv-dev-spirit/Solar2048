#nullable enable
using System.Collections.Generic;

namespace Solar2048.Buildings
{
    public sealed class BuildingsManager
    {
        private readonly List<Building> _buildings = new();
        private readonly BuildingFactory _buildingFactory;

        public BuildingsManager(BuildingFactory buildingFactory)
        {
            _buildingFactory = buildingFactory;
        }

        public void AddNewBuildingTo(BuildingType buildingType, int x, int y)
        {
            Building building = _buildingFactory.CreateBuilding(buildingType);
            _buildings.Add(building);
            building.SetPosition(x, y);
        }
    }
}