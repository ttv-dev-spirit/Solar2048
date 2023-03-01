#nullable enable
using Solar2048.AssetManagement;
using Solar2048.Buildings;
using Solar2048.SaveLoad;
using UnityEngine;

namespace Solar2048.Cards
{
    public sealed class CardSpawner : ILoadable
    {
        private readonly Card.Factory _cardFactory;
        private readonly Hand _hand;
        private readonly IAssetProvider _assetProvider;
        private readonly IBuildingSettingsProvider _buildingSettingsProvider;

        public CardSpawner(Card.Factory cardFactory, Hand hand, IAssetProvider assetProvider,
            IBuildingSettingsProvider buildingSettingsProvider, SaveController saveController)
        {
            _buildingSettingsProvider = buildingSettingsProvider;
            _assetProvider = assetProvider;
            _cardFactory = cardFactory;
            _hand = hand;
            saveController.Register(this);
        }

        public void AddCardToHand(BuildingType buildingType)
        {
            Card card = _cardFactory.Create(buildingType);
            SetCardImage(card);
            _hand.AddCard(card);
        }

        public void Load(GameData gameData)
        {
            foreach (BuildingType buildingType in gameData.Hand)
            {
                AddCardToHand(buildingType);
            }
        }

        private async void SetCardImage(Card card)
        {
            BuildingSettings buildingSettings = _buildingSettingsProvider.GetBuildingSettingsFor(card.BuildingType);
            Debug.Log($"{card.BuildingType.ToString()} = {buildingSettings.Image.AssetGUID}");
            var image = await _assetProvider.Load<Sprite>(buildingSettings.Image);
            card.SetImage(image);
        }
    }
}