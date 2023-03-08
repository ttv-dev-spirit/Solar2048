#nullable enable

using System.Collections.Generic;
using JetBrains.Annotations;
using Solar2048.Buildings;
using Solar2048.Cards;
using Solar2048.Cycles;
using Solar2048.Infrastructure;
using Solar2048.Map;
using Solar2048.Packs;
using Solar2048.Score;

namespace Solar2048.StateMachine.Game.States
{
    [UsedImplicitly]
    public class GameStateReseter : IGameStateReseter
    {
        private readonly List<IResetable> _toReset = new();

        public GameStateReseter(Hand hand, GameMap gameMap,
            ScoreCounter scoreCounter, BuildingsManager buildingsManager, CycleCounter cycleCounter,
            PackForScoreBuyer packForScoreBuyer)
        {
            _toReset.Add(hand);
            _toReset.Add(buildingsManager);
            _toReset.Add(gameMap);
            _toReset.Add(cycleCounter);
            _toReset.Add(packForScoreBuyer);
            // HACK (Stas): Here scoreCounter should be reseted after packForScoreBuyer, for  
            _toReset.Add(scoreCounter);
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