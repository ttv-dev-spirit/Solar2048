using NUnit.Framework;
using Solar2048.Buildings;
using Solar2048.Map;
using UnityEngine;

namespace Tests
{
    [TestFixture]
    public class FieldTests
    {
        [Test]
        public void WhenAddBuilding_AndFieldIsEmpty_ThenBuildingHasFieldPosition()
        {
            // Arrange.
            var position = new Vector2Int(1, 3);
            var fieldUnderTest = new Tile(position);
            var building = new Building(null);
            // Act.
            fieldUnderTest.AddBuilding(building);
            // Assert.
            Assert.IsTrue(fieldUnderTest.Building != null && fieldUnderTest.Building.Position == position);
        }

        [Test]
        public void WhenRemoveBuilding_AndFieldHadBuilding_ThenFieldIsEmpty()
        {
            // Arrange.
            var position = new Vector2Int(1, 3);
            var fieldUnderTest = new Tile(position);
            var building = new Building(null);
            fieldUnderTest.AddBuilding(building);
            // Act.
            fieldUnderTest.RemoveBuilding();
            // Assert.
            Assert.IsTrue(fieldUnderTest.Building == null);
        }
    }
}