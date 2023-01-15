#nullable enable
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Solar2048.Buildings
{
    public sealed class MouseOverObserver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private readonly ReactiveProperty<bool> _isMouseOver = new();

        public IReadOnlyReactiveProperty<bool> IsMouseOver => _isMouseOver;

        public void OnPointerEnter(PointerEventData eventData)
        {
            _isMouseOver.Value = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _isMouseOver.Value = false;
        }
    }
}