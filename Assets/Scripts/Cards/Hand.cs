#nullable enable
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Solar2048.Cards
{
    public sealed class Hand : MonoBehaviour, ICardContainer, IResetable
    {
        private readonly IReactiveProperty<Card?> _selectedCard = new ReactiveProperty<Card?>();
        private readonly List<Card> _cards = new();

        public IReadOnlyReactiveProperty<Card?> SelectedCard => _selectedCard;
        public IReadOnlyList<Card> Cards => _cards;

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
            DestroyImmediate(card.gameObject);
        }

        public void Reset()
        {
            RemoveAllCards();
            _selectedCard.Value = null;
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

        private void RemoveAllCards()
        {
            Card[] cardsToRemove = _cards.ToArray();
            for (var i = 0; i < cardsToRemove.Length; i++)
            {
                RemoveCard(cardsToRemove[i]);
            }
        }
    }

    public interface IResetable
    {
        void Reset();
    }
}