#nullable enable
using Solar2048.Buildings;
using UnityEngine;
using UnityEngine.Assertions;

namespace Solar2048
{
    public sealed class GameField
    {
        public const int FIELD_SIZE = 4;

        private readonly FieldSquare[,] _field = new FieldSquare[FIELD_SIZE, FIELD_SIZE];
        private readonly BuildingBehaviour[,] _buildings = new BuildingBehaviour[FIELD_SIZE, FIELD_SIZE];

        public void RegisterSquare(FieldSquare fieldSquare)
        {
            Vector3 position = fieldSquare.transform.position;
            var x = (int)position.x;
            int y = -(int)position.y;
            Assert.IsTrue(_field[x, y] == null);
            _field[x, y] = fieldSquare;
        }

        public bool CanAddBuildingTo(Vector2Int position)
        {
            if (!IsInsideBounds(position))
            {
                return false;
            }

            if (_buildings[position.x, position.y] != null)
            {
                return false;
            }

            return true;
        }

        private bool IsInsideBounds(Vector2Int position) =>
            position.x >= 0 && position.x < FIELD_SIZE && position.y >= 0 && position.y < FIELD_SIZE;
    }
}