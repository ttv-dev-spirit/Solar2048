﻿using System;

namespace Solar2048.StateMachine
{
    public interface IGameLifeCycle : IDisposable
    {
        void Initialize();
        void ExitGame();
        void NewGame();
    }
}