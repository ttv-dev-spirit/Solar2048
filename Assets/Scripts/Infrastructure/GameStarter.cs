#nullable enable
using Solar2048.StateMachine;
using UnityEngine;
using Zenject;

namespace Solar2048.Infrastructure
{
    public sealed class GameStarter : MonoBehaviour
    {
        private IGameLifeCycle _gameLifeCycle = null!;

        [Inject]
        private void Construct(IGameLifeCycle gameStateMachine)
        {
            _gameLifeCycle = gameStateMachine;
        }

        private void Start()
        {
            _gameLifeCycle.Initialize();
        }

        private void OnDestroy()
        {
            _gameLifeCycle.Dispose();
        }
    }
}