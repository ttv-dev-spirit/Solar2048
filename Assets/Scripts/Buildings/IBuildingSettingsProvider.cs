#nullable enable
namespace Solar2048.Buildings
{
    public interface IBuildingSettingsProvider
    {
        public BuildingSettings GetBuildingSettingsFor(BuildingType buildingType);
    }
}