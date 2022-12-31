#nullable enable
using UnityEngine;

namespace Solar2048.Buildings
{
    public sealed class BuildingFactory
    {
        private readonly BuildingFactorySettings _settings;

        public BuildingFactory(BuildingFactorySettings buildingFactorySettings)
        {
            _settings = buildingFactorySettings;
        }

        public Building CreateBuilding(BuildingType buildingType)
        {
            BuildingSettings buildingSettings = _settings.GetBuildingSettingsFor(buildingType);
            BuildingBehaviour buildingBehaviour = Object.Instantiate(_settings.BuildingPrefab)!;
            buildingBehaviour.SetImage(buildingSettings.Image);
            return new WindTurbine(buildingSettings, buildingBehaviour);
        }
    }
}