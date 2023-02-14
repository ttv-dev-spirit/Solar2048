#nullable enable
using UniRx;

namespace Solar2048.Map
{
    public readonly struct FieldStats
    {
        public readonly IReadOnlyReactiveProperty<int> Energy;
        public readonly IReadOnlyReactiveProperty<int> Water;
        public readonly IReadOnlyReactiveProperty<int> Food;

        public FieldStats(IReadOnlyReactiveProperty<int> energy, IReadOnlyReactiveProperty<int> water,
            IReadOnlyReactiveProperty<int> food)
        {
            Energy = energy;
            Water = water;
            Food = food;
        }
    }
}