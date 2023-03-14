#nullable enable

namespace Solar2048.Map.Commands
{
    public class CommandsFactory : IBuildingCommandsFactory
    {
        private readonly BuildingMergeCommand.Factory _buildingMergeCommandFactory;
        private readonly BuildingMoveCommand.Factory _buildingMoveCommandFactory;

        public CommandsFactory(BuildingMergeCommand.Factory buildingMergeCommandFactory,
            BuildingMoveCommand.Factory buildingMoveCommandFactory)
        {
            _buildingMergeCommandFactory = buildingMergeCommandFactory;
            _buildingMoveCommandFactory = buildingMoveCommandFactory;
        }

        public ICommand BuildingMerge(Tile fromTile, Tile toTile) =>
            _buildingMergeCommandFactory.Create(fromTile, toTile);

        public ICommand BuildingMove(Tile fromTile, Tile toTile) =>
            _buildingMoveCommandFactory.Create(fromTile, toTile);
    }
}