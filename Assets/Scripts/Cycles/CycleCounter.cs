#nullable enable
using JetBrains.Annotations;
using Solar2048.AssetManagement;
using Solar2048.Infrastructure;
using Solar2048.SaveLoad;
using UniRx;

namespace Solar2048.Cycles
{
    [UsedImplicitly]
    public sealed class CycleCounter : IResetable, ICycleCounter, ISavable, ILoadable
    {
        private readonly ReactiveProperty<int> _cycleCount = new();

        public IReadOnlyReactiveProperty<int> CycleCount => _cycleCount;

        public CycleCounter(SaveController saveController)
        {
            saveController.Register(this);
        }

        public void Reset()
        {
            _cycleCount.Value = 1;
        }

        public void NextCycle() => _cycleCount.Value++;
        public void Save(GameData gameData) => gameData.Cycle = _cycleCount.Value;
        public void Load(GameData gameData) => _cycleCount.Value = gameData.Cycle;
    }
}