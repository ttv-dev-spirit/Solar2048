#nullable enable
using System;
using Solar2048.Infrastructure;
using UniRx;
using UnityEngine;

namespace Solar2048.Map
{
    public sealed class GameMap : IResetable
    {
        public const int FIELD_SIZE = 4;

        private readonly Subject<Vector2Int> _onFieldClicked = new();
        private readonly Subject<Unit> _onTriggerBuildingEffects = new();
        private readonly Tile[,] _map = new Tile[FIELD_SIZE, FIELD_SIZE];
        private MapBehaviour _mapBehaviour;

        public IObservable<Vector2Int> OnFieldClicked => _onFieldClicked;
        public IObservable<Unit> OnTriggerBuildingsEffects => _onTriggerBuildingEffects;

        public GameMap(MapBehaviour mapBehaviour)
        {
            _mapBehaviour = mapBehaviour;
            CreateTiles();
        }

        public void RegisterTileBehaviour(TileBehaviour tileBehaviour)
        {
            Vector2Int position = _mapBehaviour.TileWorldToMap(tileBehaviour.transform.localPosition);
            Tile tile = GetTile(position);
            tileBehaviour.BindTile(tile);
            _map[position.x, position.y] = tile;
            tileBehaviour.OnClicked.Subscribe(FieldClickedHandler);
        }

        public void RecalculateStats()
        {
            ResetStats();
            _onTriggerBuildingEffects.OnNext(Unit.Default);
        }

        public Tile GetTile(Vector2Int position) => _map[position.x, position.y];
        public Tile GetTile(int x, int y) => _map[x, y];

        public void Reset()
        {
            for (int y = 0; y < FIELD_SIZE; y++)
            {
                for (int x = 0; x < FIELD_SIZE; x++)
                {
                    _map[x, y].Reset();
                }
            }
        }

        private void CreateTiles()
        {
            for (int x = 0; x < FIELD_SIZE; x++)
            {
                for (int y = 0; y < FIELD_SIZE; y++)
                {
                    _map[x, y] = new Tile(new Vector2Int(x, y));
                }
            }
        }

        private void ResetStats()
        {
            for (int y = 0; y < FIELD_SIZE; y++)
            {
                for (int x = 0; x < FIELD_SIZE; x++)
                {
                    _map[x, y].ResetStats();
                }
            }
        }

        private void FieldClickedHandler(TileBehaviour tileBehaviour) =>
            _onFieldClicked.OnNext(tileBehaviour.Tile.Position);

        private bool IsInsideBounds(Vector2Int position) =>
            position.x is >= 0 and < FIELD_SIZE && position.y is >= 0 and < FIELD_SIZE;
    }
}