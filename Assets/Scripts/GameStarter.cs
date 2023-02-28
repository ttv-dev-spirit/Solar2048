#nullable enable
using System;
using Solar2048.AssetManagement;
using Solar2048.StateMachine;
using UnityEngine;
using Zenject;

namespace Solar2048
{
    public sealed class GameStarter : MonoBehaviour
    {
        private GameStateMachine _gameStateMachine = null!;
        private IAssetProvider _assetProvider = null!;

        [Inject]
        private void Construct(GameStateMachine gameStateMachine, IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
            _gameStateMachine = gameStateMachine;
        }

        private void Start()
        {
            _gameStateMachine.Initialize();
        }

        private void OnDestroy()
        {
            _assetProvider.CleanUp();
        }
    }
}