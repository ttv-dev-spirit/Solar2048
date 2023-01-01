﻿#nullable enable
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Solar2048.Buildings
{
    public sealed class Hand : MonoBehaviour
    {
        private readonly IReactiveProperty<Card?> _selectedCard = new ReactiveProperty<Card?>();
        private readonly List<Card> _cards = new();

        public IReadOnlyReactiveProperty<Card?> SelectedCard => _selectedCard;

        public void AddCard(Card card)
        {
            _cards.Add(card);
            card.transform.SetParent(transform);
            card.OnClicked.Subscribe(OnCardClicked);
        }

        public void RemoveCard(Card card)
        {
            if (_selectedCard.Value == card)
            {
                _selectedCard.Value = null;
            }

            _cards.Remove(card);
            Destroy(card.gameObject);
        }

        public void UnselectCard()
        {
            if (_selectedCard.Value == null)
            {
                return;
            }

            _selectedCard.Value.Unselect();
            _selectedCard.Value = null;
        }

        private void OnCardClicked(Card clickedCard)
        {
            if (_selectedCard.Value == clickedCard)
            {
                return;
            }

            UnselectCard();

            _selectedCard.Value = clickedCard;
            _selectedCard.Value.Select();
        }
    }
}