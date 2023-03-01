#nullable enable

using System;
using Solar2048.Buildings;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Localization.Components;
using UnityEngine.UI;
using Zenject;

namespace Solar2048.Cards
{
    public sealed class Card : MonoBehaviour, IPointerClickHandler
    {
        private BuildingType _buildingType;
        private readonly Subject<Card> _onClicked = new();

        [SerializeField]
        private LocalizeStringEvent _buildingName = null!;

        [SerializeField]
        private Image _image = null!;

        [SerializeField]
        private GameObject _selectionBorder = null!;

        public BuildingType BuildingType => _buildingType;
        public IObservable<Card> OnClicked => _onClicked;

        public Image Image => _image;

        [Inject]
        private void Construct(BuildingType buildingType)
        {
            _buildingType = buildingType;
            _buildingName.SetEntry($"{buildingType.ToString()}.name");
            _selectionBorder.SetActive(false);
        }

        public void SetImage(Sprite sprite) => _image.sprite = sprite;

        public void OnPointerClick(PointerEventData eventData)
        {
            _onClicked.OnNext(this);
        }

        public void Select()
        {
            _selectionBorder.SetActive(true);
        }

        public void Unselect()
        {
            _selectionBorder.SetActive(false);
        }

        public class Factory : PlaceholderFactory<BuildingType, Card>
        {
        }
    }
}