#nullable enable

using System;
using JetBrains.Annotations;
using Solar2048.Buildings;
using Solar2048.Score;
using UniRx;
using UnityEngine;

namespace Solar2048.Map
{
    [UsedImplicitly]
    public sealed class BuildingMover : IActivatable, IBuildingMover
    {
        private readonly IBuildingsManager _buildingsManager;
        private readonly GameMap _gameMap;
        private readonly IScoreCounter _scoreCounter;

        private readonly Subject<Unit> _onMoved = new();

        public bool IsActive { get; private set; }
        public IObservable<Unit> OnMoved => _onMoved;

        public BuildingMover(GameMap gameMap, IBuildingsManager buildingsManager, IScoreCounter scoreCounter)
        {
            _gameMap = gameMap;
            _buildingsManager = buildingsManager;
            _scoreCounter = scoreCounter;
            IsActive = false;
        }

        public void MoveBuildings(MoveDirection direction)
        {
            if (!IsActive)
            {
                return;
            }

            switch (direction)
            {
                case MoveDirection.Left:
                    MoveBuildingsLeft();
                    break;
                case MoveDirection.Right:
                    MoveBuildingsRight();
                    break;
                case MoveDirection.Down:
                    MoveBuildingsDown();
                    break;
                case MoveDirection.Up:
                    MoveBuildingsUp();
                    break;
            }

            _gameMap.RecalculateStats();
            _onMoved.OnNext(Unit.Default);
        }

        public void Activate() => IsActive = true;
        public void Deactivate() => IsActive = false;

        public bool HasAnythingToMerge(MoveDirection direction)
        {
            var moveInfo = new MoveInfo(GameMap.FIELD_SIZE, GameMap.FIELD_SIZE, direction);
            for (int x = moveInfo.StartColumn; moveInfo.IsXInBounds(x); x += moveInfo.ColumnStep)
            {
                for (int y = moveInfo.StartRow; moveInfo.IsYInBounds(y); y += moveInfo.RowStep)
                {
                    Tile fromTile = _gameMap.GetTile(x, y);
                    if (fromTile.Building != null && CanBeMerged(fromTile, moveInfo))
                    {
                        return true;
                    }
                }
            }

            return false;
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

        private void MoveBuildingsDown()
        {
            for (var x = 0; x < GameMap.FIELD_SIZE; x++)
            {
                for (var y = 1; y < GameMap.FIELD_SIZE; y++)
                {
                    MoveBuildingDownAndMerge(x, y);
                }
            }
        }

        private void MoveBuildingsUp()
        {
            for (var x = 0; x < GameMap.FIELD_SIZE; x++)
            {
                for (int y = GameMap.FIELD_SIZE - 2; y >= 0; y--)
                {
                    MoveBuildingUpAndMerge(x, y);
                }
            }
        }

        private void MoveBuildingUpAndMerge(int xFrom, int yFrom)
        {
            Tile fromTile = _gameMap.GetTile(xFrom, yFrom);
            if (fromTile.Building == null)
            {
                return;
            }

            if (MergeUp(fromTile, out Tile? lastEmpty))
            {
                return;
            }

            if (lastEmpty == null)
            {
                return;
            }

            lastEmpty.AddBuilding(fromTile.Building!);
            fromTile.RemoveBuilding();
        }

        private void MoveBuildingDownAndMerge(int xFrom, int yFrom)
        {
            Tile fromTile = _gameMap.GetTile(xFrom, yFrom);
            if (fromTile.Building == null)
            {
                return;
            }

            if (MergeDown(fromTile, out Tile? lastEmpty))
            {
                return;
            }

            if (lastEmpty == null)
            {
                return;
            }

            lastEmpty.AddBuilding(fromTile.Building!);
            fromTile.RemoveBuilding();
        }

        private void MoveBuildingLeftAndMerge(int xFrom, int yFrom)
        {
            Tile fromTile = _gameMap.GetTile(xFrom, yFrom);
            if (fromTile.Building == null)
            {
                return;
            }

            if (MergeLeft(fromTile, out Tile? lastEmpty))
            {
                return;
            }

            if (lastEmpty == null)
            {
                return;
            }

            lastEmpty.AddBuilding(fromTile.Building!);
            fromTile.RemoveBuilding();
        }

        private void MoveBuildingRightAndMerge(int xFrom, int yFrom)
        {
            Tile fromTile = _gameMap.GetTile(xFrom, yFrom);
            if (fromTile.Building == null)
            {
                return;
            }

            if (MergeRight(fromTile, out Tile? lastEmpty))
            {
                return;
            }

            if (lastEmpty == null)
            {
                return;
            }

            lastEmpty.AddBuilding(fromTile.Building!);
            fromTile.RemoveBuilding();
        }

        private bool MergeUp(Tile fromTile, out Tile? lastEmpty)
        {
            lastEmpty = null;
            for (int y = fromTile.Position.y + 1; y < GameMap.FIELD_SIZE; y++)
            {
                Tile toTile = _gameMap.GetTile(fromTile.Position.x, y);
                if (toTile.Building == null)
                {
                    lastEmpty = toTile;
                    continue;
                }

                if (toTile.Building.CanBeMerged(fromTile.Building!))
                {
                    MergeBuilding(fromTile, toTile);
                    return true;
                }

                break;
            }

            return false;
        }

        private bool MergeDown(Tile fromTile, out Tile? lastEmpty)
        {
            lastEmpty = null;
            for (int y = fromTile.Position.y - 1; y >= 0; y--)
            {
                Tile toTile = _gameMap.GetTile(fromTile.Position.x, y);
                if (toTile.Building == null)
                {
                    lastEmpty = toTile;
                    continue;
                }

                if (toTile.Building.CanBeMerged(fromTile.Building!))
                {
                    MergeBuilding(fromTile, toTile);
                    return true;
                }

                break;
            }

            return false;
        }

        private bool MergeLeft(Tile fromTile, out Tile? lastEmpty)
        {
            lastEmpty = null;
            for (int x = fromTile.Position.x - 1; x >= 0; x--)
            {
                Tile toTile = _gameMap.GetTile(x, fromTile.Position.y);
                if (toTile.Building == null)
                {
                    lastEmpty = toTile;
                    continue;
                }

                if (toTile.Building.CanBeMerged(fromTile.Building!))
                {
                    MergeBuilding(fromTile, toTile);
                    return true;
                }

                break;
            }

            return false;
        }

        private bool MergeRight(Tile fromTile, out Tile? lastEmpty)
        {
            lastEmpty = null;
            for (int x = fromTile.Position.x + 1; x < GameMap.FIELD_SIZE; x++)
            {
                Tile toTile = _gameMap.GetTile(x, fromTile.Position.y);
                if (toTile.Building == null)
                {
                    lastEmpty = toTile;
                    continue;
                }

                if (toTile.Building.CanBeMerged(fromTile.Building!))
                {
                    MergeBuilding(fromTile, toTile);
                    return true;
                }

                break;
            }

            return false;
        }

        private void MergeBuilding(Tile fromTile, Tile toTile)
        {
            if (!fromTile.Building!.CanBeMerged(toTile.Building!))
            {
                return;
            }

            toTile.Building!.UpLevel();
            _buildingsManager.RemoveBuilding(fromTile.Building);
            _scoreCounter.AddMergeScore(toTile.Building.Level.Value);
        }


        private bool CanBeMerged(Tile fromTile, MoveInfo moveInfo)
        {
            if (fromTile.Building == null)
            {
                Debug.LogError(
                    $"fromTile at {fromTile.Position.ToString()} without a building should not be checked for merge.");
                return false;
            }

            for (Vector2Int toPosition = fromTile.Position + moveInfo.Direction;
                 moveInfo.IsInBounds(toPosition);
                 toPosition += moveInfo.Direction)
            {
                Tile toTile = _gameMap.GetTile(toPosition);
                if (toTile.Building == null)
                {
                    continue;
                }

                return fromTile.Building!.CanBeMerged(toTile.Building);
            }

            return false;
        }
    }
}