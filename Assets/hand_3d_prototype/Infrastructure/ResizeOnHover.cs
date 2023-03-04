﻿#nullable enable
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace hand_3d_prototype.Infrastructure
{
    public class ResizeOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private CardView _cardView = null!;

        [SerializeField]
        private float _animationDuration = 0.1f;

        [SerializeField]
        private Vector3 _resizeTo;

        private Tween? _tween;

        private void Awake()
        {
            _cardView = GetComponent<CardView>();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            ResetTween();
            _tween = _cardView.View.transform.DOScale(_resizeTo, _animationDuration);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            ResetTween();
            _tween = _cardView.View.transform.DOScale(_cardView.DefaultSize, _animationDuration);
        }

        private void ResetTween()
        {
            _tween?.Kill();
        }
    }
}