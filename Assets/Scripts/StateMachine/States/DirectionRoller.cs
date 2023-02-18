﻿#nullable enable
using Solar2048.Map;
using UnityEngine;

namespace Solar2048.StateMachine.States
{
    public sealed class DirectionRoller
    {
        public MoveDirection Roll()
        {
            int result = Random.Range(0, 4);
            return (MoveDirection)result;
        }
    }
}