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

        private readonly ReactiveProperty<int> _score = new();

        public IReadOnlyReactiveProperty<int> Score => _score;

        public ScoreCounter(StaticDataProvider staticDataProvider)
        {
            _settings = staticDataProvider.ScoreSettings;
        }

        public void AddMergeScore(int resultLevel)
        {
            int scoreToAdd = _settings.GetMergeScore(resultLevel);
            _score.Value += scoreToAdd;
        }

        public void Reset()
        {
            _score.Value = 0;
        }

        public void Save(GameData gameData)
        {
            gameData.Score = _score.Value;
        }

        public void Load(GameData gameData)
        {
            _score.Value = gameData.Score;
        }
    }
}