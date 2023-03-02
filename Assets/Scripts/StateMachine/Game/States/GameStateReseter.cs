#nullable enable
using System.Collections.Generic;
using Solar2048.Buildings;
using Solar2048.Cards;
using Solar2048.Cheats;
using Solar2048.Cycles;
using Solar2048.Infrastructure;
using Solar2048.Map;
using Solar2048.Score;

namespace Solar2048.StateMachine.Game.States
{
    public class GameStateReseter : IGameStateReseter
    {
        private List<IResetable> _toReset = new();

        public GameStateReseter(Hand hand, GameMap gameMap,
            ScoreCounter scoreCounter, BuildingsManager buildingsManager, CycleCounter cycleCounter)
        {
            _toReset.Add(scoreCounter);
            _toReset.Add(hand);
            _toReset.Add(buildingsManager);
            _toReset.Add(gameMap);
            _toReset.Add(cycleCounter);
        }

        public void Reset()
        {
            foreach (IResetable resetable in _toReset)
            {
                resetable.Reset();
            }
        }
    }
}