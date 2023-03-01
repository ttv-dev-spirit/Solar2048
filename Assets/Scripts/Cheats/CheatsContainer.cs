#nullable enable

using System.Collections.Generic;
using Solar2048.Cards;
using Solar2048.Infrastructure;

namespace Solar2048.Cheats
{
    public sealed class CheatsContainer : IActivatable, IResetable
    {
        private readonly List<ICheat> _cheats = new();

        public bool IsActive { get; private set; }

        public CheatsContainer(CheatCardsSupplier.Factory cheatCardsSupplier)
        {
            _cheats.Add(cheatCardsSupplier.Create());
        }

        public void Activate()
        {
            foreach (ICheat cheat in _cheats)
            {
                cheat.Activate();
            }

            IsActive = true;
        }

        public void Deactivate()
        {
            foreach (ICheat cheat in _cheats)
            {
                cheat.Deactivate();
            }

            IsActive = false;
        }

        public void Reset()
        {
            foreach (ICheat cheat in _cheats)
            {
                cheat.Reset();
            }
        }
    }
}