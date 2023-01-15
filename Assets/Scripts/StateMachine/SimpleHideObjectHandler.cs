#nullable enable
using Solar2048.Buildings;
using UnityEngine;

namespace Solar2048.StateMachine
{
    public class SimpleHideObjectHandler : MonoBehaviour, IHideTransitionHandler
    {
        public void OnHide()
        {
            gameObject.SetActive(false);
        }
    }
}