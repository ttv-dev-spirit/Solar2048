#nullable enable
using System;
using UniRx;
using UnityEngine;
using Zenject;

namespace Solar2048.Buildings
{
    public sealed class CheatHand : MonoBehaviour
    {
        private Hand _hand = null!;
        private CardSpawner _cardSpawner = null!;
        
        [SerializeField]
        private Transform _root = null!;

        public Card? SelectedCard { get; private set; }

        [Inject]
        private void Construct(Hand hand, CardSpawner cardSpawner)
        {
            _hand = hand;
            _cardSpawner = cardSpawner;
            _hand.SelectedCard.Subscribe(SelectedCardHandler);
        }

        private void Start()
        {
            AddCard(_cardSpawner.CreateCard(BuildingType.WindTurbine));
        }

        private void SelectedCardHandler(Card? card)
        {
            if (card == null)
            {
                return;
            }
            UnselectCard();
        }

        public void AddCard(Card card)
        {
            card.transform.SetParent(_root);
            card.OnClicked.Subscribe(OnCardClicked);
        }
        
        public void UnselectCard()
        {
            if (SelectedCard == null)
            {
                return;
            }

            SelectedCard.Unselect();
            SelectedCard = null;
        }

        private void OnCardClicked(Card clickedCard)
        {
            if (SelectedCard == clickedCard)
            {
                return;
            }

            UnselectCard();
            _hand.UnselectCard();

            SelectedCard = clickedCard;
            SelectedCard.Select();
        }

    }
}