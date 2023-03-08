#nullable enable

using JetBrains.Annotations;
using Solar2048.SaveLoad;
using UnityEngine;
using Zenject;

namespace Solar2048.StateMachine.Game.States
{
    // TODO (Stas): Handle failed load.
    [UsedImplicitly]
    public sealed class LoadGameState : State
    {
        private readonly IGameStateReseter _gameStateReseter;
        private readonly SaveController _saveController;

        public bool IsLoaded { get; private set; }

        public LoadGameState(IGameStateReseter gameStateReseter, SaveController saveController)
        {
            _saveController = saveController;
            _gameStateReseter = gameStateReseter;
        }

        protected override void OnEnter()
        {
            _gameStateReseter.Reset();
            IsLoaded = _saveController.LoadGame();
            if (!IsLoaded)
            {
                Debug.LogError($"Could not load save.");
            }

            Finish();
        }

        protected override void OnExit()
        {
        }

        [UsedImplicitly]
        public class Factory : PlaceholderFactory<LoadGameState>
        {
        }
    }
}