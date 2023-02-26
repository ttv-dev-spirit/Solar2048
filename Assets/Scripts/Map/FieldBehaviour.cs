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
        private readonly Subject<FieldBehaviour> _onClicked = new();

        [SerializeField]
        private StatDrawer _energy = null!;

        [SerializeField]
        private StatDrawer _water = null!;

        [SerializeField]
        private StatDrawer _food = null!;

        public Field Field { get; private set; } = null!;
        public IObservable<FieldBehaviour> OnClicked => _onClicked;

        [Inject]
        private void Construct(GameMap gameMap)
        {
            gameMap.RegisterFieldBehaviour(this);
        }

        public void BindField(Field field)
        {
            Field = field;
            _energy.Subscribe(field.Energy);
            _water.Subscribe(field.Water);
            _food.Subscribe(field.Food);
        }

        public void OnPointerClick(PointerEventData eventData) => _onClicked.OnNext(this);
    }
}