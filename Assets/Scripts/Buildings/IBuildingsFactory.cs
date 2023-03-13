#nullable enable
namespace Solar2048.Buildings
{
    public interface IBuildingsFactory
    {
        Building Create(BuildingType buildingType);
    }
}