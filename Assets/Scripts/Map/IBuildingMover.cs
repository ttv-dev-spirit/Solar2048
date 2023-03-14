#nullable enable
namespace Solar2048.Map
{
    public interface IBuildingMover
    {
        void MoveBuildings(MoveDirection direction);
        bool HasAnythingToMerge(MoveDirection direction);
    }
}