#nullable enable
using System.Collections.Generic;
using Solar2048.Buildings;
using Solar2048.Cards;
using Solar2048.Cheats;
using Solar2048.Infrastructure;
using Solar2048.Map;
using Solar2048.Score;
using Zenject;

namespace Solar2048.StateMachine.Game.States
{
    public sealed class NewGameState : State
    {
        private List<IResetable> _toReset = new();

        public NewGameState(Hand hand, CheatsContainer cheatsContainer, GameMap gameMap,
            ScoreCounter scoreCounter, BuildingsManager buildingsManager)
        {
            _toReset.Add(scoreCounter);
            _toReset.Add(hand);
            _toReset.Add(buildingsManager);
            _toReset.Add(gameMap);
            _toReset.Add(cheatsContainer);
        }

        protected override void OnEnter()
        {
            Reset();
            Finish();
        }

        protected override void OnExit()
        {
        }

        private void Reset()
        {
            foreach (IResetable resetable in _toReset)
            {
                resetable.Reset();
            }
        }

        public class Factory : PlaceholderFactory<NewGameState>
        {
        }
    }
}