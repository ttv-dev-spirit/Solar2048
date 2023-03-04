#nullable enable
using UnityEngine;
using UnityEngine.EventSystems;

namespace hand_3d_prototype.Infrastructure
{
    public class ResizeOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private CardView _cardView = null!;

        private bool _isResized;

        [SerializeField]
        private float _animationDuration = 0.1f;

        [SerializeField]
        private Vector3 _resizeTo;

        private void Awake()
        {
            _cardView = GetComponent<CardView>();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
        }

        public void OnPointerExit(PointerEventData eventData)
        {
        }
    }
}