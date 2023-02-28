#nullable enable
using Solar2048.StateMachine;
using UnityEngine;
using Zenject;

namespace Solar2048
{
    public sealed class GameStarter : MonoBehaviour
    {
        private GameStateMachine _gameStateMachine = null!;

        [Inject]
        private void Construct(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        private void Start()
        {
            _gameStateMachine.Initialize();
        }

        private void OnDestroy()
        {
            _gameStateMachine.Dispose();
        }
    }
}