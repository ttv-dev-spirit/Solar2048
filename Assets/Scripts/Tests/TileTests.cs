#nullable enable

using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using Solar2048.Buildings;
using Solar2048.Map;
using UnityEngine;

namespace Tests
{
    [TestFixture]
    [SuppressMessage("ReSharper", "MethodTooLong")]
    public class TileTests
    {
        [Test]
        public void WhenAddBuilding_AndTileIsEmpty_ThenBuildingHasTilePosition()
        {
            // Arrange.
            const BuildingType buildingType = BuildingType.SolarPanel;
            var position = new Vector2Int(1, 3);
            var tileUnderTest = new Tile(position);
            Building building = Create.BuildingWithoutBehaviour(buildingType);
            // Act.
            tileUnderTest.AddBuilding(building);
            // Assert.
            Assert.IsTrue(tileUnderTest.Building != null && tileUnderTest.Building.Position == position);
        }

        [Test]
        public void WhenRemoveBuilding_AndTileHadBuilding_ThenTileIsEmpty()
        {
            // Arrange.
            const BuildingType buildingType = BuildingType.SolarPanel;
            var position = new Vector2Int(1, 3);
            var tileUnderTest = new Tile(position);
            Building building = Create.BuildingWithoutBehaviour(buildingType);
            tileUnderTest.AddBuilding(building);
            // Act.
            tileUnderTest.RemoveBuilding();
            // Assert.
            Assert.IsTrue(tileUnderTest.Building == null);
        }
    }
}