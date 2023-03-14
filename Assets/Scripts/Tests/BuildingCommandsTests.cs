#nullable enable
using System;
using System.Diagnostics.CodeAnalysis;
using NSubstitute;
using NUnit.Framework;
using Solar2048.Buildings;
using Solar2048.Map;
using Solar2048.Map.Commands;
using Solar2048.SaveLoad;
using Solar2048.Score;
using UnityEngine;

namespace Tests
{
    [TestFixture]
    [SuppressMessage("ReSharper", "MethodTooLong")]
    public class BuildingCommandsTests
    {
        [Test]
        public void WhenBuildingMoveCommand_AndBuildingOnFromTile_ThenBuildingMovedToTargetTile()
        {
            // Arrange.
            const int numberOfTiles = 2;
            const BuildingType buildingType = BuildingType.SolarPanel;
            GameMap gameMap = Create.GameMap();
            Vector2Int[] randomPositions = Prepare.GetNRandomPositionsOnMap(numberOfTiles);
            var saveRegister = Substitute.For<ISaveRegister>();
            var buildingsFactory = Substitute.For<IBuildingsFactory>();
            var buildingSettings = Substitute.For<IBuildingSettings>();
            buildingSettings.BuildingType.Returns(buildingType);
            buildingsFactory.Create(buildingType).Returns(_ => new Building(buildingSettings));
            var buildingsManager = new BuildingsManager(buildingsFactory, gameMap, saveRegister);
            Tile fromTile = gameMap.GetTile(randomPositions[0]);
            Tile toTile = gameMap.GetTile(randomPositions[1]);
            Prepare.AddBuildingToPosition(gameMap, buildingsManager, fromTile.Position, buildingType);
            var moveCommandUnderTest = new BuildingMoveCommand(fromTile, toTile);

            // Act.
            moveCommandUnderTest.Execute();

            // Assert.
            Assert.IsFalse(fromTile.Building != null);
            Assert.IsTrue(toTile.Building is { BuildingType: buildingType });
        }

        [Test]
        public void WhenBuildingMergeCommand_AndIdenticalBuildingsOnFromTileAndToTile_ThenBuildingsMergedOnToTile()
        {
            // Arrange.
            const int numberOfTiles = 2;
            const BuildingType buildingType = BuildingType.SolarPanel;
            GameMap gameMap = Create.GameMap();
            Vector2Int[] randomPositions = Prepare.GetNRandomPositionsOnMap(numberOfTiles);
            var saveRegister = Substitute.For<ISaveRegister>();
            var buildingsFactory = Substitute.For<IBuildingsFactory>();
            var buildingSettings = Substitute.For<IBuildingSettings>();
            var scoreCounter = Substitute.For<IScoreCounter>();
            buildingSettings.BuildingType.Returns(buildingType);
            buildingsFactory.Create(buildingType).Returns(_ => new Building(buildingSettings));
            var buildingsManager = new BuildingsManager(buildingsFactory, gameMap, saveRegister);
            Tile fromTile = gameMap.GetTile(randomPositions[0]);
            Tile toTile = gameMap.GetTile(randomPositions[1]);
            Prepare.AddBuildingsToPositions(gameMap, buildingsManager, randomPositions, _ => buildingType);
            var mergeCommandUnderTest = new BuildingMergeCommand(fromTile, toTile, buildingsManager, scoreCounter);

            // Act.
            mergeCommandUnderTest.Execute();

            // Assert.
            Assert.IsFalse(fromTile.Building != null);
            Assert.IsTrue(toTile.Building is { BuildingType: buildingType } && toTile.Building.Level.Value == 2);
        }
    }
}