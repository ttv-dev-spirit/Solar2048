using System.Collections.Generic;
using Solar2048.Map;

namespace Solar2048.Buildings
{
    public interface IBuildingsManager
    {
        IReadOnlyList<Building> Buildings { get; }
        void AddNewBuilding(BuildingType buildingType, Tile tile);
        void RemoveBuilding(Building building);
    }
}