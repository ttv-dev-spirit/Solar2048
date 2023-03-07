#nullable enable
using System;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Solar2048.Map
{
    public sealed class TileBehaviour : MonoBehaviour, IPointerClickHandler
    {
        private readonly Subject<TileBehaviour> _onClicked = new();
        
        public Tile Tile { get; private set; } = null!;
        public IObservable<TileBehaviour> OnClicked => _onClicked;

        [Inject]
        private void Construct(GameMap gameMap)
        {
            gameMap.RegisterTileBehaviour(this);
        }

        public void BindTile(Tile tile)
        {
            Tile = tile;
        }

        public void OnPointerClick(PointerEventData eventData) => _onClicked.OnNext(this);

    }
}