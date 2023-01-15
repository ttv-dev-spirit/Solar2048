#nullable enable
using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Solar2048.Buildings
{
    public sealed class PackPreview : MonoBehaviour, IPointerClickHandler
    {
        private readonly List<BuildingImage> _buildingImages = new();
        private readonly Subject<PackPreview> _onClicked = new();

        private BuildingImage.Factory _buildingImageFactory = null!;

        [SerializeField]
        private Transform _previewRoot = null!;

        [SerializeField]
        private GameObject _selectionBorder = null!;

        public Pack? Pack { get; private set; }

        public IObservable<PackPreview> OnClicked => _onClicked;

        [Inject]
        private void Construct(BuildingImage.Factory buildingImageFactory)
        {
            _buildingImageFactory = buildingImageFactory;
        }

        public void ShowPack(Pack pack)
        {
            Pack = pack;
            foreach (BuildingType packBuildingCard in pack.BuildingCards)
            {
                AddBuildingImage(packBuildingCard);
            }
        }

        public void Hide()
        {
            Pack = null;
            foreach (BuildingImage buildingImage in _buildingImages)
            {
                Destroy(buildingImage.gameObject);
            }

            _buildingImages.Clear();
            Unselect();
        }

        public void OnPointerClick(PointerEventData eventData) => _onClicked.OnNext(this);

        public void Select()
        {
            _selectionBorder.SetActive(true);
        }

        public void Unselect()
        {
            _selectionBorder.SetActive(false);
        }
        
        private void AddBuildingImage(BuildingType buildingType)
        {
            BuildingImage buildingImage = _buildingImageFactory.Create(buildingType);
            buildingImage.transform.SetParent(_previewRoot);
            _buildingImages.Add(buildingImage);
        }

    }
}