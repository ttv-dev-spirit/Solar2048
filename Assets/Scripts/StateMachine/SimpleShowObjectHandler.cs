#nullable enable
using Solar2048.Buildings;
using UnityEngine;

namespace Solar2048.StateMachine
{
    public class SimpleShowObjectHandler : MonoBehaviour, IShowTransitionHandler
    {
        public void OnShow()
        {
            gameObject.SetActive(true);
        }
    }
}