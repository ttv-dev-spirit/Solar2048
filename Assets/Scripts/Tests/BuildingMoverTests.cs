#nullable enable

using System;
using System.Diagnostics.CodeAnalysis;
using NSubstitute;
using NUnit.Framework;
using Solar2048.Buildings;
using Solar2048.Map;
using Solar2048.Score;
using UnityEngine;

namespace Tests
{
    [TestFixture]
    [SuppressMessage("ReSharper", "MethodTooLong")]
    public class BuildingMoverTests
    {
        [Test]
        public void WhenMove_AndTwoIdenticalBuildingsAligned_ThenHasAnythingToMerge(
            [Values] MoveDirection moveDirection)
        {
            // Arrange.
            const BuildingType buildingType = BuildingType.SolarPanel;
            const int numberOfPositions = 2;
            Vector2Int[] positions = Prepare.GetNAlignedPositionsOnMap(moveDirection, numberOfPositions);
            GameMap gameMap = Create.GameMap();
            var buildingsManager = Substitute.For<IBuildingsManager>();
            var scoreCounter = Substitute.For<IScoreCounter>();
            IBuildingMover buildingsMoverUnderTest = new BuildingMover(gameMap, buildingsManager, scoreCounter);
            // Act.
            AddBuildingsToPositions(gameMap, positions, _ => buildingType);
            // Assert.
            Assert.IsTrue(buildingsMoverUnderTest.HasAnythingToMerge(moveDirection));
        }

        [Test]
        public void WhenMove_AndTwoIdenticalBuildingsNotAligned_ThenNothingToMerge(
            [Values] MoveDirection moveDirection)
        {
            // Arrange.
            const BuildingType buildingType = BuildingType.SolarPanel;
            const int numberOfPositions = 2;
            Vector2Int[] positions = Prepare.GetNUnalignedPositionsOnMap(numberOfPositions);
            GameMap gameMap = Create.GameMap();
            var buildingsManager = Substitute.For<IBuildingsManager>();
            var scoreCounter = Substitute.For<IScoreCounter>();
            IBuildingMover buildingsMoverUnderTest = new BuildingMover(gameMap, buildingsManager, scoreCounter);
            // Act.
            AddBuildingsToPositions(gameMap, positions, _ => buildingType);
            // Assert.
            Assert.IsFalse(buildingsMoverUnderTest.HasAnythingToMerge(moveDirection));
        }

        [Test]
        public void WhenMove_AndTwoDifferentBuildingsAligned_ThenNothingToMerge(
            [Values] MoveDirection moveDirection)
        {
            // Arrange.
            const BuildingType solarPanelType = BuildingType.SolarPanel;
            const BuildingType windTurbineType = BuildingType.WindTurbine;
            const int numberOfPositions = 2;
            Vector2Int[] positions = Prepare.GetNAlignedPositionsOnMap(moveDirection, numberOfPositions);
            GameMap gameMap = Create.GameMap();
            var buildingsManager = Substitute.For<IBuildingsManager>();
            var scoreCounter = Substitute.For<IScoreCounter>();
            IBuildingMover buildingsMoverUnderTest = new BuildingMover(gameMap, buildingsManager, scoreCounter);
            // Act.
            AddBuildingsToPositions(gameMap, positions, GetBuildingType);
            // Assert.
            Assert.IsFalse(buildingsMoverUnderTest.HasAnythingToMerge(moveDirection));

            BuildingType GetBuildingType(int i) => i == 0 ? solarPanelType : windTurbineType;
        }

        [Test]
        public void WhenMove_AndTwoBuildingsAlignedAsABA_ThenNothingToMerge(
            [Values] MoveDirection moveDirection)
        {
            // Arrange.
            const BuildingType solarPanelType = BuildingType.SolarPanel;
            const BuildingType windTurbineType = BuildingType.WindTurbine;
            const int numberOfPositions = 3;
            Vector2Int[] positions = Prepare.GetNAlignedPositionsOnMap(moveDirection, numberOfPositions);
            GameMap gameMap = Create.GameMap();
            var buildingsManager = Substitute.For<IBuildingsManager>();
            var scoreCounter = Substitute.For<IScoreCounter>();
            IBuildingMover buildingsMoverUnderTest = new BuildingMover(gameMap, buildingsManager, scoreCounter);
            // Act.
            AddBuildingsToPositions(gameMap, positions, GetBuildingType);

            // Assert.
            Assert.IsFalse(buildingsMoverUnderTest.HasAnythingToMerge(moveDirection));

            BuildingType GetBuildingType(int i) => i % 2 == 0 ? solarPanelType : windTurbineType;
        }

        [Test]
        public void WhenMove_AndTwoBuildingsAlignedAsBAA_ThenHasAnythingToMerge(
            [Values] MoveDirection moveDirection)
        {
            // Arrange.
            const BuildingType solarPanelType = BuildingType.SolarPanel;
            const BuildingType windTurbineType = BuildingType.WindTurbine;
            const int numberOfPositions = 3;
            Vector2Int[] positions = Prepare.GetNAlignedPositionsOnMap(moveDirection, numberOfPositions);
            GameMap gameMap = Create.GameMap();
            var buildingsManager = Substitute.For<IBuildingsManager>();
            var scoreCounter = Substitute.For<IScoreCounter>();
            IBuildingMover buildingsMoverUnderTest = new BuildingMover(gameMap, buildingsManager, scoreCounter);
            // Act.
            AddBuildingsToPositions(gameMap, positions, GetBuildingType);

            // Assert.
            Assert.IsTrue(buildingsMoverUnderTest.HasAnythingToMerge(moveDirection));

            BuildingType GetBuildingType(int i) => i == 0 ? solarPanelType : windTurbineType;
        }

        private void AddBuildingsToPositions(GameMap gameMap, Vector2Int[] positions,
            Func<int, BuildingType> getBuildingType)
        {
            for (var i = 0; i < positions.Length; i++)
            {
                Tile tile = gameMap.GetTile(positions[i]);
                BuildingType buildingType = getBuildingType(i);
                Building building = Create.BuildingWithoutBehaviour(buildingType);
                tile.AddBuilding(building);
            }
        }
    }
}