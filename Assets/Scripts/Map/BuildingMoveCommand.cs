#nullable enable
using System.Threading.Tasks;
using UnityEngine;

namespace Solar2048.Map
{
    public class BuildingMoveCommand : ICommand
    {
        private readonly Tile _fromTile;
        private readonly Tile _toTile;

        public BuildingMoveCommand(Tile fromTile, Tile toTile)
        {
            _fromTile = fromTile;
            _toTile = toTile;
        }

        public async void Execute()
        {
            if (_fromTile.Building == null)
            {
                Debug.LogError($"Trying to move building from empty tile at {_fromTile.Position.ToString()}");
            }

            _toTile.AddBuilding(_fromTile.Building!);
            _fromTile.RemoveBuilding();
        }
    }
}