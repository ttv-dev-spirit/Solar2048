#nullable enable
using System;
using Solar2048.UI;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Solar2048.Map
{
    public sealed class FieldBehaviour : MonoBehaviour, IPointerClickHandler
    {
        private readonly Subject<Unit> _onClicked = new();

        private GameFieldBehaviour _gameFieldBehaviour = null!;

        [SerializeField]
        private StatDrawer _energy = null!;

        [SerializeField]
        private StatDrawer _water = null!;

        [SerializeField]
        private StatDrawer _food = null!;

        public IObservable<Unit> OnClicked => _onClicked;

        [Inject]
        private void Construct(GameMap gameMap, GameFieldBehaviour gameFieldBehaviour)
        {
            gameMap.RegisterSquare(this);
            _gameFieldBehaviour = gameFieldBehaviour;
        }

        public void SetFieldStats(ref FieldStats fieldStats)
        {
            _energy.Subscribe(fieldStats.Energy);
            _water.Subscribe(fieldStats.Water);
            _food.Subscribe(fieldStats.Food);
        }

        public void OnPointerClick(PointerEventData eventData) => _onClicked.OnNext(Unit.Default);
    }
}