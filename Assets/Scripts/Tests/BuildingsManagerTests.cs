#nullable enable

using System.Diagnostics.CodeAnalysis;
using NSubstitute;
using NUnit.Framework;
using Solar2048.Buildings;
using Solar2048.Map;
using Solar2048.SaveLoad;
using UnityEngine;

namespace Tests
{
    [TestFixture]
    [SuppressMessage("ReSharper", "MethodTooLong")]
    public class BuildingsManagerTests
    {
        [Test]
        public void WhenAddNewBuilding_AndFieldWasEmpty_ThenBuildingIsOnTheTile()
        {
            // Arrange.
            const BuildingType buildingType = BuildingType.SolarPanel;
            var position = new Vector2Int(1, 1);
            var buildingSettings = Substitute.For<IBuildingSettings>();
            buildingSettings.BuildingType.Returns(buildingType);
            var mapBehaviour = new GameObject().AddComponent<MapBehaviour>();
            var gameMap = new GameMap(mapBehaviour);
            var saveRegister = Substitute.For<ISaveRegister>();
            var buildingsFactory = Substitute.For<IBuildingsFactory>();
            buildingsFactory.Create(buildingType).Returns(new Building(buildingSettings));
            var buildingsManager = new BuildingsManager(buildingsFactory, gameMap, saveRegister);
            // Act.
            Tile tile = gameMap.GetTile(position);
            buildingsManager.AddNewBuilding(buildingType, tile);
            // Assert.
            Assert.IsTrue(gameMap.GetTile(position).Building is { BuildingType: buildingType });
        }

        [Test]
        public void WhenAddNewBuilding_AndMapWasEmpty_ThenBuildingIsNotOnTheOtherTile()
        {
            // Arrange.
            const BuildingType buildingType = BuildingType.SolarPanel;
            var position = new Vector2Int(1, 1);
            var buildingSettings = Substitute.For<IBuildingSettings>();
            buildingSettings.BuildingType.Returns(buildingType);
            var mapBehaviour = new GameObject().AddComponent<MapBehaviour>();
            var gameMap = new GameMap(mapBehaviour);
            var saveRegister = Substitute.For<ISaveRegister>();
            var buildingsFactory = Substitute.For<IBuildingsFactory>();
            buildingsFactory.Create(buildingType).Returns(new Building(buildingSettings));
            IBuildingsManager buildingsManagerUnderTest = new BuildingsManager(buildingsFactory, gameMap, saveRegister);
            // Act.
            Tile tile = gameMap.GetTile(position);
            buildingsManagerUnderTest.AddNewBuilding(buildingType, tile);
            // Assert.
            Tile otherTile = gameMap.GetTile(2, 2);
            Assert.IsFalse(otherTile.Building is { BuildingType: buildingType });
        }

        [Test]
        public void WhenRemovedBuilding_AndFieldHadOneBuilding_ThenMapIsEmpty()
        {
            // Arrange.
            const BuildingType buildingType = BuildingType.SolarPanel;
            var position = new Vector2Int(1, 1);
            var buildingSettings = Substitute.For<IBuildingSettings>();
            buildingSettings.BuildingType.Returns(buildingType);
            var mapBehaviour = new GameObject().AddComponent<MapBehaviour>();
            var gameMap = new GameMap(mapBehaviour);
            var saveRegister = Substitute.For<ISaveRegister>();
            var buildingsFactory = Substitute.For<IBuildingsFactory>();
            buildingsFactory.Create(buildingType).Returns(new Building(buildingSettings));
            IBuildingsManager buildingsManagerUnderTest = new BuildingsManager(buildingsFactory, gameMap, saveRegister);
            // Act.
            Tile tile = gameMap.GetTile(position);
            buildingsManagerUnderTest.AddNewBuilding(buildingType, tile);
            buildingsManagerUnderTest.RemoveBuilding(tile.Building!);
            // Assert.
            for (var x = 0; x < GameMap.FIELD_SIZE; x++)
            {
                for (var y = 0; y < GameMap.FIELD_SIZE; y++)
                {
                    Assert.IsTrue(gameMap.GetTile(x, y).Building == null, $"tile {position.ToString()} has building.");
                }
            }
        }
    }
}