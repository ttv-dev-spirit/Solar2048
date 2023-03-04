#nullable enable
using UnityEngine;
using UnityEngine.EventSystems;

namespace hand_3d_prototype.Infrastructure
{
    public class Test : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        public void OnDrag(PointerEventData eventData)
        {
            if (Camera.main == null)
            {
                return;
            }

            Vector3 mousePosition = Input.mousePosition;
            Vector3 cardPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            cardPosition.z = -1;
            cardPosition.y -= 3.5f * 1.2f / 2f;
            transform.position = cardPosition;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
        }

        public void OnEndDrag(PointerEventData eventData)
        {
        }
    }
}