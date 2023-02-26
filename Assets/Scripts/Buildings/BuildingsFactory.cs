#nullable enable
using Solar2048.Buildings.UI;
using Solar2048.Map;

namespace Solar2048.Buildings
{
    // TODO (Stas): Rework to DI
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
            var building = new Building(buildingSettings, _gameMap);
            buildingBehaviour.BindBuilding(building);
            return building;
        }
    }
}