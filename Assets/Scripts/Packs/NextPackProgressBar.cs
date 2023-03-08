#nullable enable
using Solar2048.Score;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Solar2048.Packs
{
    public sealed class NextPackProgressBar : MonoBehaviour
    {
        private PackForScoreBuyer _packForScoreBuyer = null!;
        private IScoreCounter _scoreCounter = null!;

        [SerializeField]
        private Image _progressBar = null!;

        [SerializeField]
        private TMP_Text _scoreText = null!;

        [Inject]
        private void Construct(IScoreCounter scoreCounter, PackForScoreBuyer packForScoreBuyer)
        {
            _scoreCounter = scoreCounter;
            _packForScoreBuyer = packForScoreBuyer;
            _scoreCounter.CurrentScore.Subscribe(OnScoreChanged);
            _packForScoreBuyer.NextPackCost.Subscribe(OnScoreChanged);
        }

        private void OnScoreChanged(int _)
        {
            _scoreText.text = $"{_scoreCounter.CurrentScore.Value}/{_packForScoreBuyer.NextPackCost.Value}";
            float packProgress = _scoreCounter.CurrentScore.Value / (float)_packForScoreBuyer.NextPackCost.Value;
            _progressBar.fillAmount = Mathf.Min(packProgress, 1);
        }
    }
}