#nullable enable

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using NSubstitute;
using NUnit.Framework;
using Solar2048;
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
            var buildingsCommandFactory = Substitute.For<IBuildingCommandsFactory>();
            IBuildingMover buildingsMoverUnderTest = new BuildingMover(gameMap, buildingsCommandFactory);
            // Act.
            Prepare.AddBuildingsToPositionsWithoutManager(gameMap, positions, _ => buildingType);
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
            var buildingsCommandFactory = Substitute.For<IBuildingCommandsFactory>();
            IBuildingMover buildingsMoverUnderTest = new BuildingMover(gameMap, buildingsCommandFactory);
            // Act.
            Prepare.AddBuildingsToPositionsWithoutManager(gameMap, positions, _ => buildingType);
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
            var buildingsCommandFactory = Substitute.For<IBuildingCommandsFactory>();
            IBuildingMover buildingsMoverUnderTest = new BuildingMover(gameMap, buildingsCommandFactory);
            // Act.
            Prepare.AddBuildingsToPositionsWithoutManager(gameMap, positions, GetBuildingType);
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
            var buildingsCommandFactory = Substitute.For<IBuildingCommandsFactory>();
            IBuildingMover buildingsMoverUnderTest = new BuildingMover(gameMap, buildingsCommandFactory);
            // Act.
            Prepare.AddBuildingsToPositionsWithoutManager(gameMap, positions, GetBuildingType);

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
            var buildingsCommandFactory = Substitute.For<IBuildingCommandsFactory>();
            IBuildingMover buildingsMoverUnderTest = new BuildingMover(gameMap, buildingsCommandFactory);
            // Act.
            Prepare.AddBuildingsToPositionsWithoutManager(gameMap, positions, GetBuildingType);

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
            // HACK (Stas): substitute is used, cause buildingManager is not used when there are no merges. shitty.
            var buildingsManager = Substitute.For<IBuildingsManager>();
            var scoreCounter = Substitute.For<IScoreCounter>();
            var buildingCommandsFactory = new Create.BuildingsCommandFactory(buildingsManager, scoreCounter);
            IBuildingMover buildingsMoverUnderTest = new BuildingMover(gameMap, buildingCommandsFactory);
            var activatable = (IActivatable)buildingsMoverUnderTest;
            activatable.Activate();
            Prepare.AddBuildingsToPositionsWithoutManager(gameMap, positions, GetBuildingType);
            // Act.
            buildingsMoverUnderTest.MoveBuildings(moveDirection);

            // Assert.
            Vector2Int[] resultPositions =
                Prepare.GetNLastAlignedPositionsOnMap(moveDirection, numberOfPositions, positions[0]);
            for (var i = 0; i < resultPositions.Length; i++)
            {
                Tile tile = gameMap.GetTile(resultPositions[i]);
                BuildingType expectedBuildingType = GetBuildingType(i);
                Assert.IsTrue(tile.Building != null,
                    $"tile at {tile.Position.ToString()} does not have a building. \n {Utility.PrintBuildingsAt(gameMap, resultPositions)}");
                Assert.IsTrue(tile.Building!.BuildingType == expectedBuildingType,
                    $"tile at {tile.Position.ToString()} is expected to have {expectedBuildingType.ToString()} as building, but has {tile.Building.BuildingType.ToString()}. \n {Utility.PrintBuildingsAt(gameMap, resultPositions)}");
            }

            BuildingType GetBuildingType(int i) => i % 2 == 0 ? solarPanelType : windTurbineType;
        }

        [Test]
        public void WhenMove_AndNIdenticalAlignedBuildingsOnMap_ThenBuildingsAreMovedAndMerged(
            [Values] MoveDirection moveDirection, [NUnit.Framework.Range(1, 4)] int numberOfPositions)
        {
            // Arrange.
            const BuildingType buildingType = BuildingType.SolarPanel;
            Vector2Int[] positions = Prepare.GetNAlignedPositionsOnMap(moveDirection, numberOfPositions);
            GameMap gameMap = Create.GameMap();
            var saveRegister = Substitute.For<ISaveRegister>();
            var buildingsFactory = Substitute.For<IBuildingsFactory>();
            var buildingSettings = Substitute.For<IBuildingSettings>();
            buildingSettings.BuildingType.Returns(buildingType);
            buildingsFactory.Create(buildingType).Returns(_ => new Building(buildingSettings));
            var buildingsManager = new BuildingsManager(buildingsFactory, gameMap, saveRegister);
            var scoreCounter = Substitute.For<IScoreCounter>();
            var buildingCommandsFactory = new Create.BuildingsCommandFactory(buildingsManager, scoreCounter);
            IBuildingMover buildingsMoverUnderTest = new BuildingMover(gameMap, buildingCommandsFactory);
            var activatable = (IActivatable)buildingsMoverUnderTest;
            activatable.Activate();
            Prepare.AddBuildingsToPositions(gameMap, buildingsManager, positions, _ => buildingType);
            // Act.
            buildingsMoverUnderTest.MoveBuildings(moveDirection);

            // Assert.
            int resultingBuildingsNumber = numberOfPositions % 2 + numberOfPositions / 2;
            Vector2Int[] resultPositions =
                Prepare.GetNLastAlignedPositionsOnMap(moveDirection, resultingBuildingsNumber, positions[0]);
            for (var i = 0; i < resultingBuildingsNumber; i++)
            {
                Tile tile = gameMap.GetTile(resultPositions[i]);
                BuildingType expectedBuildingType = buildingType;
                Assert.IsTrue(tile.Building != null,
                    $"tile at {tile.Position.ToString()} does not have a building. \n {Utility.PrintBuildingsAt(gameMap, resultPositions)}");
                Assert.IsTrue(tile.Building!.BuildingType == expectedBuildingType,
                    $"tile at {tile.Position.ToString()} is expected to have {expectedBuildingType.ToString()} as building, but has {tile.Building.BuildingType.ToString()}. \n {Utility.PrintBuildingsAt(gameMap, resultPositions)}");
            }
        }

        [Test]
        public void WhenMove_AndNIdenticalAlignedBuildingsOnMap_ThenNHalfMergedBuildingsAreOnMap(
            [Values] MoveDirection moveDirection, [NUnit.Framework.Range(1, 4)] int numberOfPositions)
        {
            // Arrange.
            const BuildingType buildingType = BuildingType.SolarPanel;
            Vector2Int[] positions = Prepare.GetNAlignedPositionsOnMap(moveDirection, numberOfPositions);
            GameMap gameMap = Create.GameMap();
            var saveRegister = Substitute.For<ISaveRegister>();
            var buildingsFactory = Substitute.For<IBuildingsFactory>();
            var buildingSettings = Substitute.For<IBuildingSettings>();
            buildingSettings.BuildingType.Returns(buildingType);
            buildingsFactory.Create(buildingType).Returns(_ => new Building(buildingSettings));
            var buildingsManager = new BuildingsManager(buildingsFactory, gameMap, saveRegister);
            var scoreCounter = Substitute.For<IScoreCounter>();
            var buildingCommandsFactory = new Create.BuildingsCommandFactory(buildingsManager, scoreCounter);
            IBuildingMover buildingsMoverUnderTest = new BuildingMover(gameMap, buildingCommandsFactory);
            var activatable = (IActivatable)buildingsMoverUnderTest;
            activatable.Activate();
            Prepare.AddBuildingsToPositions(gameMap, buildingsManager, positions, _ => buildingType);
            // Act.
            buildingsMoverUnderTest.MoveBuildings(moveDirection);

            // Assert.
            int resultingBuildingsNumber = numberOfPositions % 2 + numberOfPositions / 2;

            var alignedPositions = new List<Vector2Int>();
            var buildingsNumber = 0;
            if (moveDirection == MoveDirection.Right || moveDirection == MoveDirection.Left)
            {
                int y = positions[0].y;
                for (var x = 0; x < GameMap.FIELD_SIZE; x++)
                {
                    Tile tile = gameMap.GetTile(x, y);
                    alignedPositions.Add(tile.Position);
                    if (tile.Building != null)
                    {
                        buildingsNumber++;
                    }
                }
            }
            else
            {
                int x = positions[0].x;
                for (var y = 0; y < GameMap.FIELD_SIZE; y++)
                {
                    Tile tile = gameMap.GetTile(x, y);
                    alignedPositions.Add(tile.Position);
                    if (tile.Building != null)
                    {
                        buildingsNumber++;
                    }
                }
            }

            Assert.IsTrue(buildingsNumber == resultingBuildingsNumber,
                $"Expected {resultingBuildingsNumber} buildings, but {buildingsNumber} is on the map. \n {Utility.PrintBuildingsAt(gameMap, alignedPositions)}");
        }
    }
}