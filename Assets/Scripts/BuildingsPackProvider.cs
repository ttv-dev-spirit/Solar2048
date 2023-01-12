#nullable enable
using System.Collections.Generic;
using Solar2048.Buildings;

namespace Solar2048
{
    public sealed class BuildingsPackProvider : IBuildingsPackProvider
    {
        public List<BuildingType> GetBuildings(int count)
        {
            return new List<BuildingType>()
            {
                BuildingType.Greenhouse,
                BuildingType.Greenhouse,
                BuildingType.Greenhouse
            };
        }
    }
}