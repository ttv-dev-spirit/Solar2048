#nullable enable
using System;
using Solar2048.Buildings;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace Solar2048
{
    public sealed class Field
    {
        private readonly FieldBehaviour _behaviour;

        private readonly Subject<Field> _onClicked = new();
        private readonly IReactiveProperty<int> _energy = new ReactiveProperty<int>();
        private readonly IReactiveProperty<int> _water = new ReactiveProperty<int>();
        private readonly IReactiveProperty<int> _food = new ReactiveProperty<int>();

        public IObservable<Field> OnClicked => _onClicked;
        public IReadOnlyReactiveProperty<int> Energy => _energy;
        public IReadOnlyReactiveProperty<int> Water => _water;
        public IReadOnlyReactiveProperty<int> Food => _food;
        public Vector2Int Position { get; }
        public Building? Building { get; private set; }

        public Field(FieldBehaviour behaviour, Vector2Int position)
        {
            _behaviour = behaviour;
            Position = position;
            var fieldStats = new FieldStats(_energy, _water, _food);
            _behaviour.SetFieldStats(ref fieldStats);
            _behaviour.OnClicked.Subscribe(ClickedHandler);
        }

        public void AddBuilding(Building building)
        {
            Assert.IsTrue(Building == null);
            Building = building;
            building.SetPosition(Position);
        }

        public void RemoveBuilding()
        {
            if (Building == null)
            {
                return;
            }

            Building = null;
        }

        public void ResetStats()
        {
            _energy.Value = 0;
            _water.Value = 0;
            _food.Value = 0;
        }

        public void AddEnergy(int value) => _energy.Value += value;
        public void AddWater(int value) => _water.Value += value;
        public void AddFood(int value) => _food.Value += value;

        private void ClickedHandler(Unit _) => _onClicked.OnNext(this);
    }
}