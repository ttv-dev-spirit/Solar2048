#nullable enable
using Solar2048.AssetManagement;
using Solar2048.Packs.UI;
using Solar2048.SaveLoad;
using Solar2048.Score;
using Solar2048.StaticData;
using Solar2048.UI;
using UniRx;

namespace Solar2048.Packs
{
    public sealed class PackForScoreBuyer : ISavable, ILoadable
    {
        private readonly PackBuyingSettings _packBuyingSettings;
        private readonly ScoreCounter _scoreCounter;
        private readonly UIManager _uiManager;
        private readonly ReactiveProperty<bool> _isEnoughPointsForPack = new();

        private int _packsBought;

        public IReadOnlyReactiveProperty<bool> IsEnoughPointsForPack => _isEnoughPointsForPack;
        public int NextPackCost => _packBuyingSettings.GetPackCost(_packsBought);

        public PackForScoreBuyer(StaticDataProvider staticDataProvider, ScoreCounter scoreCounter, UIManager uiManager,
            SaveController saveController)
        {
            _packBuyingSettings = staticDataProvider.PackBuyingSettings;
            _scoreCounter = scoreCounter;
            _uiManager = uiManager;
            _scoreCounter.CurrentScore.Subscribe(ScoreUpdateHandler);
            saveController.Register(this);
        }

        public void BuyPack()
        {
            int packCost = NextPackCost;
            _packsBought++;
            _scoreCounter.SubtractScore(packCost);
            var packSelectionScreen = _uiManager.GetScreen<IPackSelectionScreen>();
            packSelectionScreen.Show();
        }

        private void ScoreUpdateHandler(int score)
        {
            int packCost = NextPackCost;
            _isEnoughPointsForPack.Value = packCost <= _scoreCounter.CurrentScore.Value;
        }

        public void Save(GameData gameData)
        {
            gameData.PacksBought = _packsBought;
            _scoreCounter.Save(gameData);
        }

        public void Load(GameData gameData)
        {
            _packsBought = gameData.PacksBought;
            _scoreCounter.Load(gameData);
        }
    }
}