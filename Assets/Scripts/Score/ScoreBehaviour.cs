#nullable enable

using Solar2048.Packs;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

namespace Solar2048.Score
{
    public sealed class ScoreBehaviour : MonoBehaviour
    {
        private IScoreCounter _scoreCounter = null!;
        private PackForScoreBuyer _packForScoreBuyer = null!;

        [SerializeField]
        private TMP_Text _score = null!;

        [Inject]
        private void Construct(IScoreCounter scoreCounter, PackForScoreBuyer packForScoreBuyer)
        {
            _packForScoreBuyer = packForScoreBuyer;
            _scoreCounter = scoreCounter;
            _scoreCounter.Score.Subscribe(UpdateScore);
            _packForScoreBuyer.NextPackCost.Subscribe(UpdateScore);
        }

        private void UpdateScore(int _)
        {
            _score.text = $"{_scoreCounter.Score.Value}/{_packForScoreBuyer.NextPackCost.Value}";
        }
    }
}