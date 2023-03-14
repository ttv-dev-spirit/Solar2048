#nullable enable
using System.Collections.Generic;
using System.Text;
using Solar2048.Map;
using UnityEngine;

namespace Tests
{
    public static class Utility
    {
        public static string PrintBuildingsAt(GameMap gameMap, IEnumerable<Vector2Int> tilePositions)
        {
            var result = new StringBuilder();
            foreach (Vector2Int position in tilePositions)
            {
                Tile tile = gameMap.GetTile(position);
                if (tile.Building == null)
                {
                    result.AppendLine($"tile at {tile.Position.ToString()} has no building.");
                }
                else
                {
                    result.AppendLine(
                        $"tile at {tile.Position.ToString()} has {tile.Building.BuildingType.ToString()} level {tile.Building.Level}");
                }
            }

            return result.ToString();
        }
    }
}