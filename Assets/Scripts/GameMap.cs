#nullable enable
using System;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace Solar2048
{
    public sealed class GameMap
    {
        public const int FIELD_SIZE = 4;

        private readonly Subject<Vector2Int> _onFieldClicked = new();
        private readonly Field[,] _map = new Field[FIELD_SIZE, FIELD_SIZE];

        public IObservable<Vector2Int> OnFieldClicked => _onFieldClicked;

        public void RegisterSquare(FieldBehaviour fieldBehaviour)
        {
            Vector3 position = fieldBehaviour.transform.position;
            var x = (int)position.x;
            int y = -(int)position.y;
            Assert.IsTrue(_map[x, y] == null);
            var field = new Field(fieldBehaviour, new Vector2Int(x, y));
            _map[x, y] = field;
            field.OnClicked.Subscribe(FieldClickedHandler);
        }

        public bool CanAddBuildingTo(Vector2Int position)
        {
            if (!IsInsideBounds(position))
            {
                return false;
            }

            return _map[position.x, position.y].Building == null;
        }

        public Field GetField(Vector2Int position) => _map[position.x, position.y];

        private void FieldClickedHandler(Field field) => _onFieldClicked.OnNext(field.Position);

        private bool IsInsideBounds(Vector2Int position) =>
            position.x is >= 0 and < FIELD_SIZE && position.y is >= 0 and < FIELD_SIZE;
    }
}