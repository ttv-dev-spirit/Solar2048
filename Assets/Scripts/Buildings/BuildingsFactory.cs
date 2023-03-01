#nullable enable
using Solar2048.Buildings.UI;

namespace Solar2048.Buildings
{
    // TODO (Stas): Rework to DI
    public sealed class BuildingsFactory
    {
        private readonly IBuildingSettingsProvider _settings;
        private readonly BuildingBehaviour.Factory _buildingBehaviourFactory;

        public BuildingsFactory(IBuildingSettingsProvider buildingFactorySettings,
            BuildingBehaviour.Factory buildingBehaviourFactory)
        {
            _settings = buildingFactorySettings;
            _buildingBehaviourFactory = buildingBehaviourFactory;
        }

        public Building Create(BuildingType buildingType)
        {
            BuildingSettings buildingSettings = _settings.GetBuildingSettingsFor(buildingType);
            BuildingBehaviour buildingBehaviour = _buildingBehaviourFactory.Create();
            buildingBehaviour.SetImage(buildingSettings.Image);
            var building = new Building(buildingSettings);
            buildingBehaviour.BindBuilding(building);
            return building;
        }
    }
}