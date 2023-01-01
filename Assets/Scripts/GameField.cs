#nullable enable
using System;
using Solar2048.Buildings;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;
using Zenject;

namespace Solar2048
{
    public sealed class GameField
    {
        private readonly Subject<Vector2Int> _onFieldClicked = new();

        public const int FIELD_SIZE = 4;

        private GameFieldBehaviour _gameFieldBehaviour = null!;

        private readonly FieldSquare[,] _field = new FieldSquare[FIELD_SIZE, FIELD_SIZE];
        private readonly BuildingBehaviour[,] _buildings = new BuildingBehaviour[FIELD_SIZE, FIELD_SIZE];

        public IObservable<Vector2Int> OnFieldClicked => _onFieldClicked;

        [Inject]
        private void Construct(GameFieldBehaviour gameFieldBehaviour)
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
    }
}