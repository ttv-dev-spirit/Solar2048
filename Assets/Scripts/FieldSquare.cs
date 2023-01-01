#nullable enable
using System;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Solar2048
{
    public sealed class FieldSquare : MonoBehaviour, IPointerClickHandler
    {
        private readonly Subject<FieldSquare> _onClicked = new();

        private GameFieldBehaviour _gameFieldBehaviour = null!;

        public IObservable<FieldSquare> OnClicked => _onClicked;

        [Inject]
        private void Construct(GameField gameField, GameFieldBehaviour gameFieldBehaviour)
        {
            gameField.RegisterSquare(this);
            _gameFieldBehaviour = gameFieldBehaviour;
        }

        public void OnPointerClick(PointerEventData eventData) => _onClicked.OnNext(this);
    }
}