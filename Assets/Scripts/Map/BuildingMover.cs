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

        private readonly bool[,] _mergeMap = new bool[GameMap.FIELD_SIZE, GameMap.FIELD_SIZE];
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

            ResetMergeMap();
            var moveInfo = new MoveInfo(GameMap.FIELD_SIZE, GameMap.FIELD_SIZE, direction);
            for (int x = moveInfo.StartColumn; moveInfo.IsXInBounds(x); x += moveInfo.ColumnStep)
            {
                for (int y = moveInfo.StartRow; moveInfo.IsYInBounds(y); y += moveInfo.RowStep)
                {
                    MoveBuilding(x, y, ref moveInfo);
                }
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
                    if (fromTile.Building != null && HasAlignedBuildingToMerge(fromTile, ref moveInfo))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private void MoveBuilding(int x, int y, ref MoveInfo moveInfo)
        {
            Tile fromTile = _gameMap.GetTile(x, y);
            if (fromTile.Building == null)
            {
                return;
            }

            Tile targetTile = FindTargetTile(fromTile, ref moveInfo);
            if (targetTile == fromTile)
            {
                return;
            }

            if (targetTile.Building == null)
            {
                MoveBuilding(fromTile, targetTile);
                return;
            }

            MergeBuilding(fromTile, targetTile);
        }

        private void MoveBuilding(Tile fromTile, Tile toTile)
        {
            toTile.AddBuilding(fromTile.Building!);
            fromTile.RemoveBuilding();
        }

        private void MergeBuilding(Tile fromTile, Tile toTile)
        {
            toTile.Building!.UpLevel();
            _buildingsManager.RemoveBuilding(fromTile.Building);
            _scoreCounter.AddMergeScore(toTile.Building.Level.Value);
            _mergeMap[toTile.Position.x, toTile.Position.y] = true;
        }

        private bool HasAlignedBuildingToMerge(Tile fromTile, ref MoveInfo moveInfo)
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

        private Tile FindTargetTile(Tile fromTile, ref MoveInfo moveInfo)
        {
            if (fromTile.Building == null)
            {
                Debug.LogError(
                    $"fromTile at {fromTile.Position.ToString()} without a building should not be checked for merge.");
                return fromTile;
            }

            Tile lastPossiblePosition = fromTile;
            for (Vector2Int toPosition = fromTile.Position + moveInfo.Direction;
                 moveInfo.IsInBounds(toPosition);
                 toPosition += moveInfo.Direction)
            {
                Tile toTile = _gameMap.GetTile(toPosition);
                if (toTile.Building != null)
                {
                    return CanBeUniquelyMerged(fromTile, toTile) ? toTile : lastPossiblePosition;
                }

                lastPossiblePosition = toTile;
            }

            return lastPossiblePosition;
        }

        private bool CanBeUniquelyMerged(Tile fromTile, Tile toTile) =>
            fromTile.Building != null
            && toTile.Building != null
            && fromTile.Building.CanBeMerged(toTile.Building)
            && !_mergeMap[toTile.Position.x, toTile.Position.y];

        private void ResetMergeMap() => Array.Clear(_mergeMap, 0, _mergeMap.Length);
    }
}