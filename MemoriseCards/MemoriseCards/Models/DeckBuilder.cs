using System.Linq;
using MemoriseCards.Data;

namespace MemoriseCards.Models
{
    public class DeckBuilder
    {
        private readonly MemoriseCardsDbContext _context;
        private int _currentCardId;

        public DeckBuilder(MemoriseCardsDbContext context)
        {
            _context = context;
        }

        public Deck CreateNewDeck()
        {
            var deck = new Deck();
            _context.Deck.Add(deck);

            var suits = new[] { "Hearts", "Diamonds", "Clubs", "Spades" };
            var ranks = new[] { "Ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King" };

            foreach (var suit in suits)
            {
                foreach (var rank in ranks)
                {
                    var card = new Card(_currentCardId, suit, rank);
                    _context.Card.Add(card);
                    _currentCardId++;
                }
            }

            _context.SaveChanges();
            return deck;
        }

        public void ClearDecks()
        {
            var cards = _context.Card.ToList();
            _context.Card.RemoveRange(cards);

            var decks = _context.Deck.ToList();
            _context.Deck.RemoveRange(decks);

            _context.SaveChanges();
        }
    }
}