#nullable enable
using UnityEngine;
using Zenject;

namespace Solar2048.Buildings
{
    public sealed class BuildingsFactory
    {
        private readonly BuildingFactorySettings _settings;
        private readonly BuildingBehaviour.Factory _buildingBehaviourFactory;
        private readonly GameMap _gameMap;

        public BuildingsFactory(BuildingFactorySettings buildingFactorySettings,
            BuildingBehaviour.Factory buildingBehaviourFactory, GameMap gameMap)
        {
            _settings = buildingFactorySettings;
            _gameMap = gameMap;
            _buildingBehaviourFactory = buildingBehaviourFactory;
        }

        public Building Create(BuildingType buildingType)
        {
            BuildingSettings buildingSettings = _settings.GetBuildingSettingsFor(buildingType);
            BuildingBehaviour buildingBehaviour = _buildingBehaviourFactory.Create();
            buildingBehaviour.SetImage(buildingSettings.Image);
            return new Building(buildingSettings, buildingBehaviour, _gameMap);
        }
    }
}