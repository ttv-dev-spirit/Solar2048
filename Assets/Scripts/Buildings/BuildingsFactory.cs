#nullable enable
using Solar2048.Buildings.UI;
using Solar2048.Map;
using UnityEngine;
using Zenject;

namespace Solar2048.Buildings
{
    public sealed class BuildingsFactory
    {
        private readonly IBuildingSettingsContainer _settings;
        private readonly BuildingBehaviour.Factory _buildingBehaviourFactory;
        private readonly GameMap _gameMap;

        public BuildingsFactory(IBuildingSettingsContainer buildingFactorySettings,
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