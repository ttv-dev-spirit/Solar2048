#nullable enable

namespace Solar2048.Map.Commands
{
    public interface IBuildingCommandsFactory
    {
        ICommand BuildingMerge(Tile fromTile, Tile toTile);
        ICommand BuildingMove(Tile fromTile, Tile toTile);
    }
}