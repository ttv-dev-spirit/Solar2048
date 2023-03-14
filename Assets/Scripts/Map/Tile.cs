#nullable enable
using Solar2048.Buildings;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace Solar2048.Map
{
    public sealed class Tile
    {
        private readonly IReactiveProperty<int> _energy = new ReactiveProperty<int>();
        private readonly IReactiveProperty<int> _water = new ReactiveProperty<int>();
        private readonly IReactiveProperty<int> _food = new ReactiveProperty<int>();

        public IReadOnlyReactiveProperty<int> Energy => _energy;
        public IReadOnlyReactiveProperty<int> Water => _water;
        public IReadOnlyReactiveProperty<int> Food => _food;
        public Vector2Int Position { get; }
        public Building? Building { get; private set; }

        public Tile(Vector2Int position)
        {
            Position = position;
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
    }
}