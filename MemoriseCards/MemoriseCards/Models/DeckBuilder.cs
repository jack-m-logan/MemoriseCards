using System.Linq;
using System.Linq.Expressions;
using MemoriseCards.Data;
using Microsoft.EntityFrameworkCore;
using MemoriseCards.Services;

namespace MemoriseCards.Models
{
    public class DeckBuilder
    {
        private readonly MemoriseCardsDbContext _context;
        public ReviewCardsService reviewPileService;

        private int _currentCardId = 1;
        private int _currentDeckId = 1;

        public DeckBuilder(MemoriseCardsDbContext context)
        {
            _context = context;
            reviewPileService = new ReviewCardsService();

        }

        public Deck CreateNewDeck(string name)
        {
            int maxCardId = GetMaxId(_context, "Card");
            _currentCardId = maxCardId + 1;

            int maxDeckId = GetMaxId(_context, "Deck");
            _currentDeckId = maxDeckId + 1;

            var deck = new Deck(name, _currentDeckId);
            _context.Deck.Add(deck);

            var suits = new[] { "Hearts", "Diamonds", "Clubs", "Spades" };
            var ranks = new[] { "Ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King" };

            int position = 1;

            foreach (var suit in suits)
            {
                foreach (var rank in ranks)
                {
                    var card = new Card(_currentCardId, suit, rank);
                    card.OriginalPosition = position++;
                    deck.Cards.Add(card);
                    _context.Card.Add(card);

                    _currentCardId++;
                }
            }

            _context.SaveChanges();

            return deck;
        }

        public Deck RenameDeck(int deckId, string newName)
        {
            UpdateDeckProperty(deckId, d => d.Name = newName);
            return GetDeckById(deckId);
        }

        public Deck AddOrEditNotes(int deckId, string notes)
        {
            UpdateDeckProperty(deckId, d => d.Notes = notes);
            return GetDeckById(deckId);
        }

        public Deck ShuffleDeck(int deckId)
        {
            var deck = GetDeckById(deckId);

            var rng = new Random();

            for (int i = deck.Cards.Count - 1; i > 0; i--)
            {
                int j = rng.Next(i + 1);
                Card temp = deck.Cards[i];
                deck.Cards[i] = deck.Cards[j];
                deck.Cards[i].OriginalPosition = i + 1;
                deck.Cards[j] = temp;
                deck.Cards[j].OriginalPosition = j + 1;
            }

            return deck;
        }

        public Deck ReorderDeck(int deckId)
        {
            var deck = GetDeckById(deckId);

            deck.Cards = deck.Cards.OrderBy(c => c.Id).ToList();

            return deck;
        }

        public Card DrawOneCard(int deckId)
        {
            var deck = GetDeckById(deckId);

            var card = deck.Cards.First();

            AddCardToReviewPile(card);

            deck.Cards.Remove(card); // Remove the drawn card from the list of cards in the deck

            return card;
        }

        #region Helpers

        private int GetMaxId(DbContext context, string tableName)
        {
            var entity = context.Model.FindEntityType(typeof(Card).FullName);

            var primaryKey = entity?.FindPrimaryKey();
            var idProperty = primaryKey?.Properties[0];

            if (entity != null && primaryKey != null && idProperty != null)
            {
                var parameter = Expression.Parameter(entity.ClrType, "id");
                var property = Expression.PropertyOrField(parameter, idProperty.Name);
                var lambda = Expression.Lambda<Func<Card, int>>(property, parameter);

                var queryable = context.Set<Card>().AsQueryable();
                var maxId = queryable.Any() ? queryable.Max(lambda) : 0;

                return maxId;
            }
            else
            {
                throw new Exception("There was an error retrieving the max ID.");
            }
        }

        private Deck GetDeckById(int deckId)
        {
            var deck = _context.Deck.Find(deckId);

            if (deck != null)
            {
                return deck;
            }

            return null;
        }

        private void UpdateDeckProperty(int deckId, Action<Deck> updateAction)
        {
            var deck = GetDeckById(deckId);

            if (deck != null)
            {
                updateAction(deck);
                _context.SaveChanges();
            }
        }

        public bool IsDeckShuffled(Deck deck)
        {
            for (int i = 0; i < deck.Cards.Count; i++)
            {
                if (deck.Cards[i].OriginalPosition != i + 1)
                {
                    return false;
                }
            }

            return true;
        }

        public void AddCardToReviewPile(Card card)
        {
            reviewPileService.AddToReviewPile(card);
        }

        #endregion
    }
}