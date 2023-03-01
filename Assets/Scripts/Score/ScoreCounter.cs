#nullable enable
using Solar2048.Cards;
using UniRx;
using UnityEngine;

namespace Solar2048.Score
{
    public sealed class ScoreCounter : IResetable
    {
        private readonly ReactiveProperty<int> _totalScore = new();
        private readonly ReactiveProperty<int> _currentScore = new();
        private readonly ScoreSettings _settings;

        public IReadOnlyReactiveProperty<int> TotalScore => _totalScore;
        public IReadOnlyReactiveProperty<int> CurrentScore => _currentScore;

        public ScoreCounter(ScoreSettings settings)
        {
            _settings = settings;
        }

        public void AddMergeScore(int resultLevel)
        {
            int scoreToAdd = _settings.MergeScore * (int)Mathf.Pow(2, resultLevel - 2);
            _totalScore.Value += scoreToAdd;
            _currentScore.Value += scoreToAdd;
        }

        public void SubtractScore(int value)
        {
            _currentScore.Value -= value;
        }

        public void Reset()
        {
            _totalScore.Value = 0;
            _currentScore.Value = 0;
        }
    }
}