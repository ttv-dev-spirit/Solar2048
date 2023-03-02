using UniRx;

namespace Solar2048.Cycles
{
    public interface ICycleCounter
    {
        IReadOnlyReactiveProperty<int> CycleCount { get; }
        void NextCycle();
    }
}