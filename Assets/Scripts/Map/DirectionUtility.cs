#nullable enable
using System;
using UnityEngine;

namespace Solar2048.Map
{
    public static class DirectionUtility
    {
        public static Vector2Int ToVector(this MoveDirection direction) =>
            direction switch
            {
                MoveDirection.Up => Vector2Int.up,
                MoveDirection.Right => Vector2Int.right,
                MoveDirection.Down => Vector2Int.down,
                MoveDirection.Left => Vector2Int.left,
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
            };
    }
}