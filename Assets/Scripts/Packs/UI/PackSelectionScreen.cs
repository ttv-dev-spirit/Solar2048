#nullable enable
using Solar2048.Buildings;
using Solar2048.Cards;
using Solar2048.UI;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Solar2048.Packs.UI
{
    public sealed class PackSelectionScreen : UIScreen
    {
        private PackGenerator _packGenerator = null!;
        private CardSpawner _cardSpawner = null!;
        private PackPreview? _selectedPack;

        [SerializeField]
        private PackPreview _leftPackPreview = null!;

        [SerializeField]
        private PackPreview _rightPackPreview = null!;

        [SerializeField]
        private Button _selectButton = null!;

        [Inject]
        private void Construct(PackGenerator packGenerator, CardSpawner cardSpawner)
        {
            _packGenerator = packGenerator;
            _cardSpawner = cardSpawner;
        }

        private void Start()
        {
            _leftPackPreview.OnClicked.Subscribe(OnPackClicked);
            _rightPackPreview.OnClicked.Subscribe(OnPackClicked);
            _selectButton.OnClickAsObservable().Subscribe(OnSelectClicked);
        }

        protected override void OnShow()
        {
            Pack leftPack = _packGenerator.GetPack();
            Pack rightPack = _packGenerator.GetPack();
            _leftPackPreview.ShowPack(leftPack);
            _rightPackPreview.ShowPack(rightPack);
            _selectButton.interactable = false;
        }

        protected override void OnHide()
        {
            _leftPackPreview.Hide();
            _rightPackPreview.Hide();
            _selectedPack = null;
        }

        private void OnPackClicked(PackPreview packPreview)
        {
            if (_selectedPack != null)
            {
                _selectedPack.Unselect();
            }

            _selectedPack = packPreview;
            _selectedPack.Select();
            _selectButton.interactable = true;
        }

        private void OnSelectClicked(Unit _)
        {
            if (_selectedPack == null)
            {
                return;
            }

            var cards = _selectedPack.Pack!.BuildingCards;
            foreach (BuildingType card in cards)
            {
                _cardSpawner.AddCardToHand(card);
            }
            Hide();
        }
    }
}