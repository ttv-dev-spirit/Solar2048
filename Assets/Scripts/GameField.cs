#nullable enable
using System;
using Solar2048.Buildings;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace Solar2048
{
    public sealed class GameField
    {
        public const int FIELD_SIZE = 4;

        private readonly GameFieldBehaviour _gameFieldBehaviour;

        private readonly Subject<Vector2Int> _onFieldClicked = new();
        private readonly FieldSquare[,] _field = new FieldSquare[FIELD_SIZE, FIELD_SIZE];
        private readonly Building?[,] _buildings = new Building[FIELD_SIZE, FIELD_SIZE];

        private BuildingMover _buildingMover;
        private BuildingsManager _buildingsManager; 

        public IObservable<Vector2Int> OnFieldClicked => _onFieldClicked;

        public GameField(GameFieldBehaviour gameFieldBehaviour)
        {
            _gameFieldBehaviour = gameFieldBehaviour;
        }

        public void RegisterSquare(FieldSquare fieldSquare)
        {
            Vector3 position = fieldSquare.transform.position;
            var x = (int)position.x;
            int y = -(int)position.y;
            Assert.IsTrue(_field[x, y] == null);
            _field[x, y] = fieldSquare;
            fieldSquare.OnClicked.Subscribe(FieldClickedHandler);
        }

        public void RegisterBuilding(Building building, Vector2Int position)
        {
            _buildings[position.x, position.y] = building;
        }

        public void MoveBuildings(MoveDirections direction) => _buildingMover.MoveBuildings(direction);

        public bool CanAddBuildingTo(Vector2Int position)
        {
            if (!IsInsideBounds(position))
            {
                return false;
            }

            if (_buildings[position.x, position.y] != null)
            {
                return false;
            }

            return true;
        }


        private void FieldClickedHandler(FieldSquare fieldSquare)
        {
            Vector2Int position = _gameFieldBehaviour.GetFieldPosition(fieldSquare);
            _onFieldClicked.OnNext(position);
        }

        private bool IsInsideBounds(Vector2Int position) =>
            position.x >= 0 && position.x < FIELD_SIZE && position.y >= 0 && position.y < FIELD_SIZE;

        // HACK (Stas): This is the shit.
        public void InjectManager(BuildingsManager buildingsManager)
        {
            _buildingsManager = buildingsManager;
            _buildingMover = new BuildingMover(_buildings, FIELD_SIZE, buildingsManager);
        }
    }
}