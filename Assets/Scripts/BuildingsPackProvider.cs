#nullable enable
using System.Collections.Generic;
using Solar2048.Buildings;
using UnityEngine;

namespace Solar2048
{
    public sealed class BuildingsPackProvider : IBuildingsPackProvider
    {
        private readonly List<BuildingType> _possibleBuildings = new()
        {
            BuildingType.Greenhouse,
            BuildingType.SolarPanel,
            BuildingType.WaterCollector,
            BuildingType.WindTurbine
        };

        public List<BuildingType> GetBuildings(int count)
        {
            var result = new List<BuildingType>();
            for (var i = 0; i < count; i++)
            {
                var buildingIndex = (int)(Random.value * _possibleBuildings.Count);
                result.Add(_possibleBuildings[buildingIndex]);
            }

            return result;
        }
    }
}