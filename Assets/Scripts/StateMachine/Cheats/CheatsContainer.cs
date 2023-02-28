#nullable enable

using System.Collections.Generic;

namespace Solar2048.StateMachine.Cheats
{
    public sealed class CheatsContainer : IActivatable
    {
        private readonly List<IActivatable> _cheats = new();

        public bool IsActive { get; private set; }

        public CheatsContainer(CheatCardsSupplier.Factory cheatCardsSupplier)
        {
            _cheats.Add(cheatCardsSupplier.Create());
        }

        public void Activate()
        {
            foreach (IActivatable cheat in _cheats)
            {
                cheat.Activate();
            }

            IsActive = true;
        }

        public void Deactivate()
        {
            foreach (IActivatable cheat in _cheats)
            {
                cheat.Deactivate();
            }

            IsActive = false;
        }
    }
}