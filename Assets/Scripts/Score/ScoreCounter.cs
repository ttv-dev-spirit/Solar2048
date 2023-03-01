#nullable enable

using Solar2048.AssetManagement;
using Solar2048.Cards;
using Solar2048.Infrastructure;
using Solar2048.SaveLoad;
using Solar2048.StaticData;
using UniRx;
using UnityEngine;

namespace Solar2048.Score
{
    public sealed class ScoreCounter : IResetable, ISavable, ILoadable
    {
        private readonly ReactiveProperty<int> _totalScore = new();
        private readonly ReactiveProperty<int> _currentScore = new();
        private readonly ScoreSettings _settings;

        public IReadOnlyReactiveProperty<int> TotalScore => _totalScore;
        public IReadOnlyReactiveProperty<int> CurrentScore => _currentScore;

        public ScoreCounter(StaticDataProvider staticDataProvider, SaveController saveController)
        {
            _settings = staticDataProvider.ScoreSettings;
            saveController.Register(this);
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