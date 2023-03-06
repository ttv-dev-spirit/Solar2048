#nullable enable
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

namespace Solar2048.Score
{
    public sealed class ScoreBehaviour : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _score = null!;

        [Inject]
        private void Construct(IScoreCounter scoreCounter)
        {
            scoreCounter.TotalScore.Subscribe(OnScoreChanged);
        }

        private void OnScoreChanged(int score) => _score.text = score.ToString();
    }
}