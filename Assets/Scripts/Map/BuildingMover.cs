#nullable enable

using System;
using System.Collections.Generic;
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
        private readonly Building?[,] _moveMap = new Building[GameMap.FIELD_SIZE, GameMap.FIELD_SIZE];
        private readonly List<ICommand> _commands = new();
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

        public async void MoveBuildings(MoveDirection direction)
        {
            if (!IsActive)
            {
                return;
            }

            ResetMaps();
            _commands.Clear();

            var moveInfo = new MoveInfo(GameMap.FIELD_SIZE, GameMap.FIELD_SIZE, direction);
            for (int x = moveInfo.StartColumn; moveInfo.IsXInBounds(x); x += moveInfo.ColumnStep)
            {
                for (int y = moveInfo.StartRow; moveInfo.IsYInBounds(y); y += moveInfo.RowStep)
                {
                    MoveBuilding(x, y, ref moveInfo);
                }
            }

            foreach (ICommand command in _commands)
            {
                command.Execute();
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

            Vector2Int targetTilePosition = FindTargetTile(fromTile, ref moveInfo);
            if (fromTile.Position == targetTilePosition)
            {
                _moveMap[targetTilePosition.x, targetTilePosition.y] = fromTile.Building;
                return;
            }

            Building? targetTileBuilding = _moveMap[targetTilePosition.x, targetTilePosition.y];

            if (targetTileBuilding == null)
            {
                MoveBuilding(fromTile, targetTilePosition);
                return;
            }

            MergeBuilding(fromTile, targetTilePosition);
        }

        private void MoveBuilding(Tile fromTile, Vector2Int toPosition)
        {
            _moveMap[toPosition.x, toPosition.y] = fromTile.Building;
            Tile toTile = _gameMap.GetTile(toPosition);
            _commands.Add(new BuildingMoveCommand(fromTile, toTile));
        }

        private void MergeBuilding(Tile fromTile, Vector2Int toPosition)
        {
            _mergeMap[toPosition.x, toPosition.y] = true;
            Tile toTile = _gameMap.GetTile(toPosition);
            _commands.Add(new BuildingMergeCommand(fromTile, toTile, _buildingsManager, _scoreCounter));
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

        private Vector2Int FindTargetTile(Tile fromTile, ref MoveInfo moveInfo)
        {
            Vector2Int lastPossiblePosition = fromTile.Position;

            if (fromTile.Building == null)
            {
                Debug.LogError(
                    $"fromTile at {fromTile.Position.ToString()} without a building should not be checked for merge.");
                return lastPossiblePosition;
            }

            for (Vector2Int toPosition = fromTile.Position + moveInfo.Direction;
                 moveInfo.IsInBounds(toPosition);
                 toPosition += moveInfo.Direction)
            {
                if (_moveMap[toPosition.x, toPosition.y] != null)
                {
                    return CanBeUniquelyMerged(fromTile, toPosition) ? toPosition : lastPossiblePosition;
                }

                lastPossiblePosition = toPosition;
            }

            return lastPossiblePosition;
        }

        private bool CanBeUniquelyMerged(Tile fromTile, Vector2Int toPosition)
        {
            Building? destinationBuilding = _moveMap[toPosition.x, toPosition.y];
            return fromTile.Building != null
                   && destinationBuilding != null
                   && fromTile.Building.CanBeMerged(destinationBuilding)
                   && !_mergeMap[toPosition.x, toPosition.y];
        }

        private void ResetMaps()
        {
            Array.Clear(_mergeMap, 0, _mergeMap.Length);
            Array.Clear(_moveMap, 0, _moveMap.Length);
        }
    }
}