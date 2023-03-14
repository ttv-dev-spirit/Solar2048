#nullable enable

using NSubstitute;
using Solar2048.Buildings;
using Solar2048.Cards;
using Solar2048.Map;
using Solar2048.Map.Commands;
using Solar2048.Score;
using UnityEngine;

namespace Tests
{
    public static class Create
    {
        public static Hand Hand() => new GameObject().AddComponent<Hand>();
        public static Card Card() => new GameObject().AddComponent<Card>();

        public static GameMap GameMap()
        {
            var mapBehaviour = new GameObject().AddComponent<MapBehaviour>();
            var gameMap = new GameMap(mapBehaviour);
            return gameMap;
        }

        public static Building BuildingWithoutBehaviour(BuildingType buildingType)
        {
            var buildingSettings = Substitute.For<IBuildingSettings>();
            buildingSettings.BuildingType.Returns(buildingType);
            var building = new Building(buildingSettings);
            return building;
        }

        // FIXME: There should be a better way.
        public class BuildingsCommandFactory : IBuildingCommandsFactory
        {
            private readonly IBuildingsManager _buildingsManager;
            private readonly IScoreCounter _scoreCounter;

            public BuildingsCommandFactory(IBuildingsManager buildingsManager,
                IScoreCounter scoreCounter)
            {
                _scoreCounter = scoreCounter;
                _buildingsManager = buildingsManager;
            }

            public ICommand BuildingMerge(Tile fromTile, Tile toTile) =>
                new BuildingMergeCommand(fromTile, toTile, _buildingsManager, _scoreCounter);

            public ICommand BuildingMove(Tile fromTile, Tile toTile) => new BuildingMoveCommand(fromTile, toTile);
        }
    }
}