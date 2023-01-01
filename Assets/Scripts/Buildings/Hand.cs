#nullable enable
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Solar2048.Buildings
{
    public sealed class Hand : MonoBehaviour
    {
        private readonly List<Card> _cards = new();

        private Card? _selectedCard;

        public void AddCard(Card card)
        {
            _cards.Add(card);
            card.transform.SetParent(transform);
            card.OnClicked.Subscribe(OnCardClicked);
        }

        private void OnCardClicked(Card clickedCard)
        {
            if (_selectedCard == clickedCard)
            {
                return;
            }

            if (_selectedCard != null)
            {
                _selectedCard.Unselect();
            }

            _selectedCard = clickedCard;
            _selectedCard.Select();
        }
    }
}