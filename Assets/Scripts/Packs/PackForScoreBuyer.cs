#nullable enable
using Solar2048.Cards;
using Solar2048.Packs.UI;
using Solar2048.Score;
using Solar2048.StaticData;
using Solar2048.UI;
using UniRx;

namespace Solar2048.Packs
{
    public sealed class PackForScoreBuyer
    {
        private readonly PackBuyingSettings _packBuyingSettings;
        private readonly ScoreCounter _scoreCounter;
        private readonly UIManager _uiManager;
        private readonly ReactiveProperty<bool> _isEnoughPointsForPack = new();

        public IReadOnlyReactiveProperty<bool> IsEnoughPointsForPack => _isEnoughPointsForPack;
        public int NextPackCost => _packBuyingSettings.PackCost;

        public PackForScoreBuyer(StaticDataProvider staticDataProvider, ScoreCounter scoreCounter, UIManager uiManager)
        {
            _packBuyingSettings = staticDataProvider.PackBuyingSettings;
            _scoreCounter = scoreCounter;
            _uiManager = uiManager;
            _scoreCounter.CurrentScore.Subscribe(ScoreUpdateHandler);
        }

        public void BuyPack()
        {
            int packCost = _packBuyingSettings.PackCost;
            _scoreCounter.SubtractScore(packCost);
            var packSelectionScreen = _uiManager.GetScreen<IPackSelectionScreen>();
            packSelectionScreen.Show();
        }

        private void ScoreUpdateHandler(int score)
        {
            int packCost = _packBuyingSettings.PackCost;
            _isEnoughPointsForPack.Value = packCost <= _scoreCounter.CurrentScore.Value;
        }
    }
}