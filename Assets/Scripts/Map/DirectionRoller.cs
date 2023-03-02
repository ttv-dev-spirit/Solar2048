#nullable enable
using UnityEngine;

namespace Solar2048.Map
{
    public sealed class DirectionRoller : IDirectionRoller
    {
        public MoveDirection Roll()
        {
            int result = Random.Range(0, 4);
            return (MoveDirection)result;
        }
    }
}