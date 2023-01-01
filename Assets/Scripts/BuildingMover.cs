#nullable enable
using Solar2048.Buildings;
using UnityEngine;

namespace Solar2048
{
    public sealed class BuildingMover
    {
        private readonly BuildingsManager _buildingsManager;
        private readonly GameMap _gameMap;

        public BuildingMover(GameMap gameMap, BuildingsManager buildingsManager)
        {
            _gameMap = gameMap;
            _buildingsManager = buildingsManager;
        }

        public void MoveBuildings(MoveDirections directions)
        {
            switch (directions)
            {
                case MoveDirections.Left:
                    MoveBuildingsLeft();
                    break;
                case MoveDirections.Right:
                    MoveBuildingsRight();
                    break;
                case MoveDirections.Up:
                    MoveBuildingsUp();
                    break;
                case MoveDirections.Down:
                    MoveBuildingsDown();
                    break;
            }
        }

        private void MoveBuildingsLeft()
        {
            for (var y = 0; y < GameMap.FIELD_SIZE; y++)
            {
                MoveRowLeft(y);
            }
        }

        private void MoveBuildingsRight()
        {
            for (var y = 0; y < GameMap.FIELD_SIZE; y++)
            {
                MoveRowRight(y);
            }
        }

        private void MoveBuildingsUp()
        {
            for (var x = 0; x < GameMap.FIELD_SIZE; x++)
            {
                MoveColumnUp(x);
            }
        }

        private void MoveBuildingsDown()
        {
            for (var x = 0; x < GameMap.FIELD_SIZE; x++)
            {
                MoveColumnDown(x);
            }
        }

        private void MoveRowLeft(int row)
        {
            for (var x = 1; x < GameMap.FIELD_SIZE; x++)
            {
                MoveBuilding(new Vector2Int(x, row), new Vector2Int(x - 1, row));
            }
        }

        private void MoveRowRight(int row)
        {
            for (int x = GameMap.FIELD_SIZE - 2; x >= 0; x--)
            {
                MoveBuilding(new Vector2Int(x,row), new Vector2Int(x+1,row));
            }
        }
        
        private void MoveColumnUp(int column)
        {
            for (var y = 1; y < GameMap.FIELD_SIZE; y++)
            {
                MoveBuilding(new Vector2Int(column, y), new Vector2Int(column,y-1));
            }
        }
        
        private void MoveColumnDown(int column)
        {
            for (int y = GameMap.FIELD_SIZE - 2; y >= 0; y--)
            {
                MoveBuilding(new Vector2Int(column,y), new Vector2Int(column, y+1));
            }
        }
        
        private void MoveBuilding(Vector2Int from, Vector2Int to)
        {
            Field fromField = _gameMap.GetField(from);
            if (fromField.Building == null)
            {
                return;
            }

            Field toField = _gameMap.GetField(to);
            if (toField.Building != null)
            {
                MergeBuilding(fromField, toField);
                return;
            }

            toField.AddBuilding(fromField.Building);
            fromField.RemoveBuilding();
        }

        private void MergeBuilding(Field fromField, Field toField)
        {
            if (!fromField.Building!.CanBeMerged(toField.Building!))
            {
                return;
            }

            toField.Building!.UpLevel();
            _buildingsManager.RemoveBuilding(fromField.Building);
            fromField.RemoveBuilding();
        }
    }
}