#nullable enable
namespace Solar2048.Buildings
{
    public interface IBuildingSettingsContainer
    {
        public BuildingSettings GetBuildingSettingsFor(BuildingType buildingType);
    }
}