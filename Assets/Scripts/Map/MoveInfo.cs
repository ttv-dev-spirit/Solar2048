#nullable enable

using System;
using UnityEngine;

namespace Solar2048.Map
{
    public readonly struct MoveInfo
    {
        public readonly int Rows;
        public readonly int Columns;
        public readonly int StartRow;
        public readonly int StartColumn;
        public readonly int RowStep;
        public readonly int ColumnStep;
        public readonly Vector2Int Direction;

        // Мы указываем в каком порядке рассматривать ячейки матрицы, т.е. если движение будет направо, то рассматриваем ячейки справа налево.
        public MoveInfo(int rows, int columns, MoveDirection direction)
        {
            Rows = rows;
            Columns = columns;
            Direction = direction.ToVector();

            switch (direction)
            {
                case MoveDirection.Up:
                    StartRow = Rows - 1;
                    RowStep = -1;
                    StartColumn = 0;
                    ColumnStep = 1;
                    break;
                case MoveDirection.Down:
                    StartRow = 0;
                    RowStep = 1;
                    StartColumn = 0;
                    ColumnStep = 1;
                    break;
                case MoveDirection.Right:
                    StartRow = 0;
                    RowStep = 1;
                    StartColumn = Columns - 1;
                    ColumnStep = -1;
                    break;
                case MoveDirection.Left:
                    StartRow = 0;
                    RowStep = 1;
                    StartColumn = 0;
                    ColumnStep = 1;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }

        public bool IsXInBounds(int x) => x >= 0 && x < Columns;
        public bool IsYInBounds(int y) => y >= 0 && y < Rows;
        public bool IsInBounds(Vector2Int position) => IsXInBounds(position.x) && IsYInBounds(position.y);
    }
}