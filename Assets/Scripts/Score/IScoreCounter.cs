using UniRx;

namespace Solar2048.Score
{
    public interface IScoreCounter
    {
        void AddMergeScore(int resultLevel);
        void SubtractScore(int value);
        IReadOnlyReactiveProperty<int> TotalScore { get; }
        IReadOnlyReactiveProperty<int> CurrentScore { get; }
    }
}