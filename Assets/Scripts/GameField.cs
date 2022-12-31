#nullable enable
using UnityEngine;
using UnityEngine.Assertions;

namespace Solar2048
{
    public sealed class GameField
    {
        public const int FIELD_SIZE = 4;

        private readonly FieldSquare[,] _field = new FieldSquare[FIELD_SIZE, FIELD_SIZE];

        public void RegisterSquare(FieldSquare fieldSquare)
        {
            Vector3 position = fieldSquare.transform.position;
            var x = (int)position.x;
            int y = -(int)position.y;
            Assert.IsTrue(_field[x, y] == null);
            _field[x, y] = fieldSquare;
        }
    }
}