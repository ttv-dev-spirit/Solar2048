#nullable enable
using Solar2048.StateMachine;
using UniRx;
using UnityEngine;

namespace Solar2048
{
    public sealed class ScoreCounter
    {
        private readonly ReactiveProperty<int> _score = new();
        private readonly ScoreSettings _settings;

        public IReadOnlyReactiveProperty<int> Score => _score;

        public ScoreCounter(ScoreSettings settings, IMessageReceiver messageReceiver)
        {
            _settings = settings;
            messageReceiver.Receive<NewGameMessage>().Subscribe(OnNewGame);
        }

        public void AddMergeScore(int resultLevel)
        {
            _score.Value += _settings.MergeScore * (int)Mathf.Pow(2, resultLevel - 2);
        }

        private void OnNewGame(NewGameMessage message)
        {
            _score.Value = 0;
        }
    }
}