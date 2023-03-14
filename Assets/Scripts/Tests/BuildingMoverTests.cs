#nullable enable

using System;
using System.Diagnostics.CodeAnalysis;
using NSubstitute;
using NUnit.Framework;
using Solar2048;
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
        public void WhenHasAnythingToMerge_AndTwoIdenticalBuildingsAligned_ThenHasAnythingToMerge(
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
        public void WhenHasAnythingToMerge_AndTwoIdenticalBuildingsNotAligned_ThenNothingToMerge(
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
        public void WhenHasAnythingToMerge_AndTwoDifferentBuildingsAligned_ThenNothingToMerge(
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
        public void WhenHasAnythingToMerge_AndTwoBuildingsAlignedAsABA_ThenNothingToMerge(
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
        public void WhenHasAnythingToMerge_AndTwoBuildingsAlignedAsBAA_ThenHasAnythingToMerge(
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

        [Test]
        public void WhenMove_AndNDifferentAlignedBuildingsOnMap_ThenBuildingsAreMoved(
            [Values] MoveDirection moveDirection, [NUnit.Framework.Range(1, 4)] int numberOfPositions)
        {
            // Arrange.
            const BuildingType solarPanelType = BuildingType.SolarPanel;
            const BuildingType windTurbineType = BuildingType.WindTurbine;
            Vector2Int[] positions = Prepare.GetNAlignedPositionsOnMap(moveDirection, numberOfPositions);
            GameMap gameMap = Create.GameMap();
            var buildingsManager = Substitute.For<IBuildingsManager>();
            var scoreCounter = Substitute.For<IScoreCounter>();
            IBuildingMover buildingsMoverUnderTest = new BuildingMover(gameMap, buildingsManager, scoreCounter);
            var activatable = (IActivatable)buildingsMoverUnderTest;
            activatable.Activate();
            AddBuildingsToPositions(gameMap, positions, GetBuildingType);
            // Act.
            buildingsMoverUnderTest.MoveBuildings(moveDirection);

            // Assert.
            Vector2Int[] resultPositions =
                Prepare.GetNLastAlignedPositions(moveDirection, numberOfPositions, positions[0]);
            for (var i = 0; i < resultPositions.Length; i++)
            {
                Tile tile = gameMap.GetTile(resultPositions[i]);
                BuildingType expectedBuildingType = GetBuildingType(i);
                Assert.IsTrue(tile.Building != null,
                    $"tile at {tile.Position.ToString()} does not have a building.");
                Assert.IsTrue(tile.Building!.BuildingType == expectedBuildingType,
                    $"tile at {tile.Position.ToString()} is expected to have {expectedBuildingType.ToString()} as building, but has {tile.Building.BuildingType.ToString()}. \n {Utility.PrintBuildingsAt(gameMap, resultPositions)}");
            }

            BuildingType GetBuildingType(int i) => i % 2 == 0 ? solarPanelType : windTurbineType;
        }

        private void AddBuildingsToPositions(GameMap gameMap, Vector2Int[] positions,
            Func<int, BuildingType> getBuildingType, bool log = false)
        {
            for (var i = 0; i < positions.Length; i++)
            {
                Tile tile = gameMap.GetTile(positions[i]);
                BuildingType buildingType = getBuildingType(i);
                Building building = Create.BuildingWithoutBehaviour(buildingType);
                tile.AddBuilding(building);
                if (log)
                {
                    Debug.Log($"{building.BuildingType.ToString()} is added to {tile.Position.ToString()}");
                }
            }
        }
    }
}