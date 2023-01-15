#nullable enable
using Solar2048.Buildings;
using UniRx;

namespace Solar2048
{
    public sealed class PackForScoreBuyer
    {
        private readonly PackBuyingSettings _packBuyingSettings;
        private readonly ScoreCounter _scoreCounter;
        private readonly UIManager _uiManager;
        private readonly ReactiveProperty<bool> _isEnoughPointsForPack = new();

        public IReadOnlyReactiveProperty<bool> IsEnoughPointsForPack => _isEnoughPointsForPack;

        public PackForScoreBuyer(PackBuyingSettings packBuyingSettings, ScoreCounter scoreCounter, UIManager uiManager)
        {
            _packBuyingSettings = packBuyingSettings;
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