using UniRx;

namespace Solar2048.Score
{
    public interface IScoreCounter
    {
        void AddMergeScore(int resultLevel);
        IReadOnlyReactiveProperty<int> Score { get; }
    }
}