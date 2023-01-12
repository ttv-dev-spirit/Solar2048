﻿#nullable enable
using Solar2048.Buildings;

namespace Solar2048
{
    public sealed class BuildingMover
    {
        private readonly BuildingsManager _buildingsManager;
        private readonly GameMap _gameMap;
        private readonly ScoreCounter _scoreCounter;

        public BuildingMover(GameMap gameMap, BuildingsManager buildingsManager, ScoreCounter scoreCounter)
        {
            _gameMap = gameMap;
            _buildingsManager = buildingsManager;
            _scoreCounter = scoreCounter;
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

            _gameMap.RecalculateStats();
        }

        private void MoveBuildingsLeft()
        {
            for (var y = 0; y < GameMap.FIELD_SIZE; y++)
            {
                for (var x = 1; x < GameMap.FIELD_SIZE; x++)
                {
                    MoveBuildingLeftAndMerge(x, y);
                }
            }
        }

        private void MoveBuildingsRight()
        {
            for (var y = 0; y < GameMap.FIELD_SIZE; y++)
            {
                for (int x = GameMap.FIELD_SIZE - 2; x >= 0; x--)
                {
                    MoveBuildingRightAndMerge(x, y);
                }
            }
        }

        private void MoveBuildingsUp()
        {
            for (var x = 0; x < GameMap.FIELD_SIZE; x++)
            {
                for (var y = 1; y < GameMap.FIELD_SIZE; y++)
                {
                    MoveBuildingUpAndMerge(x, y);
                }
            }
        }

        private void MoveBuildingsDown()
        {
            for (var x = 0; x < GameMap.FIELD_SIZE; x++)
            {
                for (int y = GameMap.FIELD_SIZE - 2; y >= 0; y--)
                {
                    MoveBuildingDownAndMerge(x, y);
                }
            }
        }

        private void MoveBuildingDownAndMerge(int xFrom, int yFrom)
        {
            Field fromField = _gameMap.GetField(xFrom, yFrom);
            if (fromField.Building == null)
            {
                return;
            }

            if (MergeDown(fromField, out Field? lastEmpty))
            {
                return;
            }

            if (lastEmpty == null)
            {
                return;
            }

            lastEmpty.AddBuilding(fromField.Building!);
            fromField.RemoveBuilding();
        }

        private void MoveBuildingUpAndMerge(int xFrom, int yFrom)
        {
            Field fromField = _gameMap.GetField(xFrom, yFrom);
            if (fromField.Building == null)
            {
                return;
            }

            if (MergeUp(fromField, out Field? lastEmpty))
            {
                return;
            }

            if (lastEmpty == null)
            {
                return;
            }

            lastEmpty.AddBuilding(fromField.Building!);
            fromField.RemoveBuilding();
        }

        private void MoveBuildingLeftAndMerge(int xFrom, int yFrom)
        {
            Field fromField = _gameMap.GetField(xFrom, yFrom);
            if (fromField.Building == null)
            {
                return;
            }

            if (MergeLeft(fromField, out Field? lastEmpty))
            {
                return;
            }

            if (lastEmpty == null)
            {
                return;
            }

            lastEmpty.AddBuilding(fromField.Building!);
            fromField.RemoveBuilding();
        }

        private void MoveBuildingRightAndMerge(int xFrom, int yFrom)
        {
            Field fromField = _gameMap.GetField(xFrom, yFrom);
            if (fromField.Building == null)
            {
                return;
            }

            if (MergeRight(fromField, out Field? lastEmpty))
            {
                return;
            }

            if (lastEmpty == null)
            {
                return;
            }

            lastEmpty.AddBuilding(fromField.Building!);
            fromField.RemoveBuilding();
        }

        private bool MergeDown(Field fromField, out Field? lastEmpty)
        {
            lastEmpty = null;
            for (int y = fromField.Position.y + 1; y < GameMap.FIELD_SIZE; y++)
            {
                Field toField = _gameMap.GetField(fromField.Position.x, y);
                if (toField.Building == null)
                {
                    lastEmpty = toField;
                    continue;
                }

                if (toField.Building.CanBeMerged(fromField.Building!))
                {
                    MergeBuilding(fromField, toField);
                    return true;
                }

                break;
            }

            return false;
        }

        private bool MergeUp(Field fromField, out Field? lastEmpty)
        {
            lastEmpty = null;
            for (int y = fromField.Position.y - 1; y >= 0; y--)
            {
                Field toField = _gameMap.GetField(fromField.Position.x, y);
                if (toField.Building == null)
                {
                    lastEmpty = toField;
                    continue;
                }

                if (toField.Building.CanBeMerged(fromField.Building!))
                {
                    MergeBuilding(fromField, toField);
                    return true;
                }

                break;
            }

            return false;
        }

        private bool MergeLeft(Field fromField, out Field? lastEmpty)
        {
            lastEmpty = null;
            for (int x = fromField.Position.x - 1; x >= 0; x--)
            {
                Field toField = _gameMap.GetField(x, fromField.Position.y);
                if (toField.Building == null)
                {
                    lastEmpty = toField;
                    continue;
                }

                if (toField.Building.CanBeMerged(fromField.Building!))
                {
                    MergeBuilding(fromField, toField);
                    return true;
                }

                break;
            }

            return false;
        }

        private bool MergeRight(Field fromField, out Field? lastEmpty)
        {
            lastEmpty = null;
            for (int x = fromField.Position.x + 1; x < GameMap.FIELD_SIZE; x++)
            {
                Field toField = _gameMap.GetField(x, fromField.Position.y);
                if (toField.Building == null)
                {
                    lastEmpty = toField;
                    continue;
                }

                if (toField.Building.CanBeMerged(fromField.Building!))
                {
                    MergeBuilding(fromField, toField);
                    return true;
                }

                break;
            }

            return false;
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
            _scoreCounter.AddMergeScore(toField.Building.Level.Value);
        }
    }
}