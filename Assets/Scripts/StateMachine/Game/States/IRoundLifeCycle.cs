namespace Solar2048.StateMachine.Game.States
{
    public interface IRoundLifeCycle
    {
        void PauseGame();
        void ResumeGame();
    }
}