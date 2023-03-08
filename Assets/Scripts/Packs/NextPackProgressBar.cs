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

        [Inject]
        private void Construct(IScoreCounter scoreCounter, PackForScoreBuyer packForScoreBuyer)
        {
            _scoreCounter = scoreCounter;
            _packForScoreBuyer = packForScoreBuyer;
            _scoreCounter.Score.Subscribe(OnScoreChanged);
            _packForScoreBuyer.NextPackCost.Subscribe(OnScoreChanged);
        }

        private void OnScoreChanged(int _)
        {
            int currentPackCost = _packForScoreBuyer.GetCurrentPackCost();
            int nextPackCost = _packForScoreBuyer.NextPackCost.Value;
            int score = _scoreCounter.Score.Value;
            float packProgress = (score - currentPackCost) / (float)(nextPackCost - currentPackCost);
            _progressBar.fillAmount = Mathf.Clamp(packProgress, 0, 1);
        }
    }
}