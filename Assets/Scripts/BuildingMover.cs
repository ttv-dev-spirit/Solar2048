#nullable enable
using Solar2048.Buildings;
using UnityEngine;

namespace Solar2048
{
    public sealed class BuildingMover
    {
        private readonly Building?[,] _buildings;
        private readonly int _fieldSize;

        public BuildingMover(Building?[,] buildings, int fieldSize)
        {
            _buildings = buildings;
            _fieldSize = fieldSize;
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
            for (var y = 0; y < _fieldSize; y++)
            {
                MoveRowLeft(y);
            }
        }

        private void MoveBuildingsRight()
        {
            for (var y = 0; y < _fieldSize; y++)
            {
                MoveRowRight(y);
            }
        }

        private void MoveBuildingsUp()
        {
            for (var x = 0; x < _fieldSize; x++)
            {
                MoveColumnUp(x);
            }
        }

        private void MoveBuildingsDown()
        {
            for (var x = 0; x < _fieldSize; x++)
            {
                MoveColumnDown(x);
            }
        }

        private void MoveRowLeft(int row)
        {
            for (var x = 1; x < _fieldSize; x++)
            {
                Building? building = _buildings[x, row];
                if (building == null)
                {
                    continue;
                }

                if (_buildings[x - 1, row] != null)
                {
                    continue;
                }

                _buildings[x - 1, row] = building;
                _buildings[x, row] = null;
                building.SetPosition(new Vector2Int(x - 1, row));
            }
        }

        private void MoveRowRight(int row)
        {
            for (var x = _fieldSize - 2; x >= 0; x--)
            {
                Building? building = _buildings[x, row];
                if (building == null)
                {
                    continue;
                }

                if (_buildings[x + 1, row] != null)
                {
                    continue;
                }

                _buildings[x + 1, row] = building;
                _buildings[x, row] = null;
                building.SetPosition(new Vector2Int(x + 1, row));
            }
        }

        private void MoveColumnUp(int column)
        {
            for (var y = 1; y < _fieldSize; y++)
            {
                Building? building = _buildings[column, y];
                if (building == null)
                {
                    continue;
                }

                if (_buildings[column, y - 1] != null)
                {
                    continue;
                }

                _buildings[column, y - 1] = building;
                _buildings[column, y] = null;
                building.SetPosition(new Vector2Int(column, y - 1));
            }
        }

        private void MoveColumnDown(int column)
        {
            for (var y = _fieldSize - 2; y >= 0; y--)
            {
                Building? building = _buildings[column, y];
                if (building == null)
                {
                    continue;
                }

                if (_buildings[column, y + 1] != null)
                {
                    continue;
                }

                _buildings[column, y + 1] = building;
                _buildings[column, y] = null;
                building.SetPosition(new Vector2Int(column, y + 1));
            }
        }
    }
}