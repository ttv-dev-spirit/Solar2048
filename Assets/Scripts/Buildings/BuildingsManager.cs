#nullable enable
using System.Collections.Generic;
using Solar2048.Map;

namespace Solar2048.Buildings
{
    public sealed class BuildingsManager
    {
        private readonly BuildingsFactory _buildingsFactory;

        private readonly List<Building> _buildings = new();

        public IReadOnlyList<Building> Buildings => _buildings;

        public BuildingsManager(BuildingsFactory buildingsFactory)
        {
            _buildingsFactory = buildingsFactory;
        }

        public void AddNewBuilding(BuildingType buildingType, Field field)
        {
            Building building = _buildingsFactory.Create(buildingType);
            _buildings.Add(building);
            field.AddBuilding(building);
        }

        public void RemoveBuilding(Building building)
        {
            _buildings.Remove(building);
            building.Destroy();
        }
    }
}