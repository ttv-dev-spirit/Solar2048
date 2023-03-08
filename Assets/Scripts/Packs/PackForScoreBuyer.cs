#nullable enable

using JetBrains.Annotations;
using Solar2048.AssetManagement;
using Solar2048.Infrastructure;
using Solar2048.Packs.UI;
using Solar2048.SaveLoad;
using Solar2048.Score;
using Solar2048.StaticData;
using Solar2048.UI;
using UniRx;

namespace Solar2048.Packs
{
    [UsedImplicitly]
    public sealed class PackForScoreBuyer : ISavable, ILoadable, IResetable
    {
        private readonly ScoreCounter _scoreCounter;
        private readonly UIManager _uiManager;
        private readonly PackBuyingSettings _packBuyingSettings;

        private readonly ReactiveProperty<bool> _isEnoughPointsForPack = new();
        private readonly ReactiveProperty<int> _nextPackCost = new();

        private int _packsBought;

        public IReadOnlyReactiveProperty<bool> IsEnoughPointsForPack => _isEnoughPointsForPack;
        public IReadOnlyReactiveProperty<int> NextPackCost => _nextPackCost;

        public PackForScoreBuyer(StaticDataProvider staticDataProvider, ScoreCounter scoreCounter, UIManager uiManager,
            SaveController saveController)
        {
            _packBuyingSettings = staticDataProvider.PackBuyingSettings;
            _scoreCounter = scoreCounter;
            _uiManager = uiManager;
            _scoreCounter.Score.Subscribe(ScoreUpdateHandler);
            _nextPackCost.Subscribe(ScoreUpdateHandler);
            saveController.Register(this);
        }

        public void BuyPack()
        {
            _packsBought++;
            _nextPackCost.Value = _packBuyingSettings.GetPackCost(_packsBought);
            var packSelectionScreen = _uiManager.GetScreen<PackSelectionScreen>();
            packSelectionScreen.Show();
        }

        private void ScoreUpdateHandler(int _)
        {
            _isEnoughPointsForPack.Value = _nextPackCost.Value <= _scoreCounter.Score.Value;
        }

        public void Save(GameData gameData)
        {
            gameData.PacksBought = _packsBought;
            _scoreCounter.Save(gameData);
        }

        public void Load(GameData gameData)
        {
            _packsBought = gameData.PacksBought;
            _nextPackCost.Value = _packBuyingSettings.GetPackCost(_packsBought);
            _scoreCounter.Load(gameData);
        }

        public void Reset()
        {
            _packsBought = 0;
            _nextPackCost.Value = _packBuyingSettings.GetPackCost(_packsBought);
        }

        public int GetCurrentPackCost() => _packBuyingSettings.GetPackCost(_packsBought - 1);
    }
}