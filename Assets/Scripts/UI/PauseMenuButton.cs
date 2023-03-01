#nullable enable
using Solar2048.StateMachine;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Solar2048.UI
{
    public sealed class PauseMenuButton : MonoBehaviour
    {
        private IGameLifeCycle _gameLifeCycle = null!;

        [SerializeField]
        private Button _menuButton = null!;

        [Inject]
        private void Construct(IGameLifeCycle gameLifeCycle)
        {
            _gameLifeCycle = gameLifeCycle;
        }

        private void Awake()
        {
            _menuButton.onClick.AddListener(OnMenu);
        }

        private void OnMenu() => _gameLifeCycle.Pause();
    }
}