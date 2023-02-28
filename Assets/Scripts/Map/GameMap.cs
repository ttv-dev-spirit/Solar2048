#nullable enable
using System;
using Solar2048.StateMachine.Messages;
using UniRx;
using UnityEngine;

namespace Solar2048.Map
{
    public sealed class GameMap
    {
        public const int FIELD_SIZE = 4;

        private readonly Subject<Vector2Int> _onFieldClicked = new();
        private readonly Subject<Unit> _onTriggerBuildingEffects = new();
        private readonly Field[,] _map = new Field[FIELD_SIZE, FIELD_SIZE];

        public IObservable<Vector2Int> OnFieldClicked => _onFieldClicked;
        public IObservable<Unit> OnTriggerBuildingsEffects => _onTriggerBuildingEffects;

        public GameMap(IMessageReceiver messageReceiver)
        {
            CreateFields();
            messageReceiver.Receive<NewGameMessage>().Subscribe(ResetMap);
        }

        public void RegisterFieldBehaviour(FieldBehaviour fieldBehaviour)
        {
            Vector3 position = fieldBehaviour.transform.position;
            var x = (int)position.x;
            int y = -(int)position.y;
            Field field = GetField(x, y);
            fieldBehaviour.BindField(field);
            _map[x, y] = field;
            fieldBehaviour.OnClicked.Subscribe(FieldClickedHandler);
        }

        public bool CanAddBuildingTo(Vector2Int position)
        {
            if (!IsInsideBounds(position))
            {
                return false;
            }

            return _map[position.x, position.y].Building == null;
        }

        public void RecalculateStats()
        {
            ResetStats();
            _onTriggerBuildingEffects.OnNext(Unit.Default);
        }

        public Field GetField(Vector2Int position) => _map[position.x, position.y];
        public Field GetField(int x, int y) => _map[x, y];

        private void CreateFields()
        {
            for (int x = 0; x < FIELD_SIZE; x++)
            {
                for (int y = 0; y < FIELD_SIZE; y++)
                {
                    _map[x, y] = new Field(new Vector2Int(x, y));
                }
            }
        }

        private void ResetMap(NewGameMessage _)
        {
            for (int y = 0; y < FIELD_SIZE; y++)
            {
                for (int x = 0; x < FIELD_SIZE; x++)
                {
                    _map[x, y].Reset();
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

        private void FieldClickedHandler(FieldBehaviour fieldBehaviour) =>
            _onFieldClicked.OnNext(fieldBehaviour.Field.Position);

        private bool IsInsideBounds(Vector2Int position) =>
            position.x is >= 0 and < FIELD_SIZE && position.y is >= 0 and < FIELD_SIZE;
    }
}