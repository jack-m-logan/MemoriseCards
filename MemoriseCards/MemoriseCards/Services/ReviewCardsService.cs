using System;
using System.Collections.Generic;
using MemoriseCards.Models;

namespace MemoriseCards.Services
{
    public class ReviewCardsService
    {
        private Queue<Card> _reviewPile;

        public ReviewCardsService()
        {
            _reviewPile = new Queue<Card>();
        }

        public void AddToReviewPile(Card card)
        {
            _reviewPile.Enqueue(card);
        }

        public Card GetNextCardFromReviewPile()
        {
            if (_reviewPile.Count > 0)
            {
                return _reviewPile.Dequeue();
            }
            else
            {
                return null;
            }
        }

        public void ClearReviewPile()
        {
            _reviewPile.Clear();
        }
    }
}
