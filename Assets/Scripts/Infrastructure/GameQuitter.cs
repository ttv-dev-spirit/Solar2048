#nullable enable
using UnityEngine;

namespace Solar2048.StateMachine
{
    public sealed class GameQuitter : IGameQuitter
    {
        // TODO (Stas): Finish for other build targets
        public void QuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}