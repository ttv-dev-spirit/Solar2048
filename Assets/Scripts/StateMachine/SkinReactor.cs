#nullable enable
using UnityEngine;

namespace Solar2048.StateMachine
{
    public abstract class SkinReactor : MonoBehaviour
    {
        public abstract void ActivateSkin(int skinID);
    }
}