#nullable enable

using JetBrains.Annotations;
using Solar2048.AssetManagement;
using Solar2048.Infrastructure;
using Solar2048.SaveLoad;
using Solar2048.StaticData;
using UniRx;

namespace Solar2048.Score
{
    [UsedImplicitly]
    public sealed class ScoreCounter : IResetable, ISavable, ILoadable, IScoreCounter
    {
        private readonly ScoreSettings _settings;
        
        private readonly ReactiveProperty<int> _totalScore = new();
        private readonly ReactiveProperty<int> _currentScore = new();

        public IReadOnlyReactiveProperty<int> TotalScore => _totalScore;
        public IReadOnlyReactiveProperty<int> CurrentScore => _currentScore;

        public ScoreCounter(StaticDataProvider staticDataProvider)
        {
            _settings = staticDataProvider.ScoreSettings;
        }

        public void AddMergeScore(int resultLevel)
        {
            int scoreToAdd = _settings.GetMergeScore(resultLevel);
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

        public void Save(GameData gameData)
        {
            gameData.CurrentScore = _currentScore.Value;
            gameData.TotalScore = _totalScore.Value;
        }

        public void Load(GameData gameData)
        {
            _currentScore.Value = gameData.CurrentScore;
            _totalScore.Value = gameData.TotalScore;
        }
    }
}