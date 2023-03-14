#nullable enable

using Solar2048.Buildings;
using Solar2048.Score;
using UnityEngine;
using Zenject;

namespace Solar2048.Map.Commands
{
    public class BuildingMergeCommand : ICommand
    {
        private readonly IBuildingsManager _buildingsManager;
        private readonly IScoreCounter _scoreCounter;

        private readonly Tile _fromTile;
        private readonly Tile _toTile;

        public BuildingMergeCommand(Tile fromTile, Tile toTile, IBuildingsManager buildingsManager,
            IScoreCounter scoreCounter)
        {
            _scoreCounter = scoreCounter;
            _fromTile = fromTile;
            _toTile = toTile;
            _buildingsManager = buildingsManager;
        }

        public async void Execute()
        {
            if (_toTile.Building == null)
            {
                Debug.LogError($"Trying to move building from empty tile at {_fromTile.Position.ToString()}");
            }

            _toTile.Building!.UpLevel();
            _buildingsManager.RemoveBuilding(_fromTile.Building);
            _scoreCounter.AddMergeScore(_toTile.Building.Level.Value);
        }

        public class Factory : PlaceholderFactory<Tile, Tile, BuildingMergeCommand>
        {
        }
    }
}