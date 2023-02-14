#nullable enable
using System;
using Solar2048.Buildings;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace Solar2048.Cards
{
    public sealed class Card : MonoBehaviour, IPointerClickHandler
    {
        private BuildingType _buildingType;
        private readonly Subject<Card> _onClicked = new();

        [SerializeField]
        private TMP_Text _buildingName = null!;

        [SerializeField]
        private Image _buildingImage = null!;

        [SerializeField]
        private GameObject _selectionBorder = null!;

        public BuildingType BuildingType => _buildingType;
        public IObservable<Card> OnClicked => _onClicked;

        [Inject]
        private void Construct(BuildingType buildingType, BuildingFactorySettings buildingFactorySettings)
        {
            _buildingType = buildingType;
            BuildingSettings buildingSettings = buildingFactorySettings.GetBuildingSettingsFor(_buildingType);
            _buildingName.text = buildingSettings.Name;
            _buildingImage.sprite = buildingSettings.Image;
            _selectionBorder.SetActive(false);
        }

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