#nullable enable
using System;
using System.Collections.Generic;
using Solar2048.Map;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Tests
{
    public static class Prepare
    {
        public static Vector2Int[] GetNUnalignedPositionsOnMap(int numberOfPositions)
        {
            if (numberOfPositions > GameMap.FIELD_SIZE)
            {
                Debug.LogError($"Number of positions is too big.");
                return Array.Empty<Vector2Int>();
            }

            var result = new List<Vector2Int>();
            for (var i = 0; i < numberOfPositions; i++)
            {
                result.Add(new Vector2Int(i, i));
            }

            return result.ToArray();
        }

        public static Vector2Int[] GetNAlignedPositionsOnMap(MoveDirection direction, int numberOfPositions)
        {
            if (numberOfPositions > GameMap.FIELD_SIZE)
            {
                Debug.LogError($"Number of positions is too big.");
                return Array.Empty<Vector2Int>();
            }

            switch (direction)
            {
                case MoveDirection.Up:
                case MoveDirection.Down:
                    return GetNVerticallyAlignedPositionsOnMap(numberOfPositions);
                case MoveDirection.Right:
                case MoveDirection.Left:
                    return GetNHorizontallyAlignedPositionsOnMap(numberOfPositions);
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }

        public static Vector2Int[] GetNLastAlignedPositions(MoveDirection direction, int numberOfPositions,
            Vector2Int position) =>
            direction switch
            {
                MoveDirection.Up => GetNAlignedPositionsFromTheTop(numberOfPositions, position),
                MoveDirection.Down => GetNAlignedPositionsFromTheBottom(numberOfPositions, position),
                MoveDirection.Right => GetNAlignedPositionsFromTheRight(numberOfPositions, position),
                MoveDirection.Left => GetNAlignedPositionsFromTheLeft(numberOfPositions, position),
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
            };

        private static Vector2Int[] GetNAlignedPositionsFromTheLeft(int numberOfPositions, Vector2Int position)
        {
            var result = new List<Vector2Int>();
            int y = position.y;
            for (var x = 0; x < numberOfPositions; x++)
            {
                result.Add(new Vector2Int(x, y));
            }

            return result.ToArray();
        }

        private static Vector2Int[] GetNAlignedPositionsFromTheRight(int numberOfPositions, Vector2Int position)
        {
            var result = new List<Vector2Int>();
            int y = position.y;
            for (int x = GameMap.FIELD_SIZE - numberOfPositions; x < GameMap.FIELD_SIZE; x++)
            {
                result.Add(new Vector2Int(x, y));
            }

            return result.ToArray();
        }

        private static Vector2Int[] GetNAlignedPositionsFromTheBottom(int numberOfPositions, Vector2Int position)
        {
            var result = new List<Vector2Int>();
            int x = position.x;
            for (var y = 0; y < numberOfPositions; y++)
            {
                result.Add(new Vector2Int(x, y));
            }

            return result.ToArray();
        }

        private static Vector2Int[] GetNAlignedPositionsFromTheTop(int numberOfPositions, Vector2Int position)
        {
            var result = new List<Vector2Int>();
            int x = position.x;
            for (int y = GameMap.FIELD_SIZE - numberOfPositions; y < GameMap.FIELD_SIZE; y++)
            {
                result.Add(new Vector2Int(x, y));
            }

            return result.ToArray();
        }

        private static Vector2Int[] GetNHorizontallyAlignedPositionsOnMap(int numberOfPositions)
        {
            var result = new List<Vector2Int>();
            int y = Random.Range(0, GameMap.FIELD_SIZE);
            for (var x = 0; x < numberOfPositions; x++)
            {
                result.Add(new Vector2Int(x, y));
            }

            return result.ToArray();
        }

        private static Vector2Int[] GetNVerticallyAlignedPositionsOnMap(int numberOfPositions)
        {
            var result = new List<Vector2Int>();
            int x = Random.Range(0, GameMap.FIELD_SIZE);
            for (var y = 0; y < numberOfPositions; y++)
            {
                result.Add(new Vector2Int(x, y));
            }

            return result.ToArray();
        }
    }
}