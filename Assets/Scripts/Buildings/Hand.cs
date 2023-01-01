#nullable enable
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Solar2048.Buildings
{
    public sealed class Hand : MonoBehaviour
    {
        private readonly List<Card> _cards = new();

        public Card? SelectedCard { get; private set; }

        public void AddCard(Card card)
        {
            _cards.Add(card);
            card.transform.SetParent(transform);
            card.OnClicked.Subscribe(OnCardClicked);
        }

        public void RemoveCard(Card card)
        {
            if (SelectedCard == card)
            {
                SelectedCard = null;
            }

            _cards.Remove(card);
            Destroy(card.gameObject);
        }

        private void OnCardClicked(Card clickedCard)
        {
            if (SelectedCard == clickedCard)
            {
                return;
            }

            if (SelectedCard != null)
            {
                SelectedCard.Unselect();
            }

            SelectedCard = clickedCard;
            SelectedCard.Select();
        }
    }
}