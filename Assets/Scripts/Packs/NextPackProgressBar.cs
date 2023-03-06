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

        [SerializeField]
        private Image _progressBar = null!;

        [SerializeField]
        private TMP_Text _scoreText = null!;

        [Inject]
        private void Construct(IScoreCounter scoreCounter, PackForScoreBuyer packForScoreBuyer)
        {
            _packForScoreBuyer = packForScoreBuyer;
            scoreCounter.CurrentScore.Subscribe(OnScoreChanged);
        }

        private void OnScoreChanged(int score)
        {
            _scoreText.text = $"{score}/{_packForScoreBuyer.NextPackCost}";
            float packProgress = score / (float)_packForScoreBuyer.NextPackCost;
            _progressBar.fillAmount = Mathf.Min(packProgress, 1);
        }
    }
}