using System;
using Xunit;
using MemoriseCards.Models;
using MemoriseCards.Data;
using Microsoft.EntityFrameworkCore;

public class DeckTests
{
    private readonly MemoriseCardsDbContext _context;

    public DeckBuilder DeckBuilder()
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING", EnvironmentVariableTarget.Process);

        var options = new DbContextOptionsBuilder<MemoriseCardsDbContext>()
            .UseNpgsql(connectionString)
            .Options;

        var context = new MemoriseCardsDbContext(options, configuration);

        return new DeckBuilder(context);
    }

    [Fact]
    public void TestCreateNewDeck()
    {
        // Arrange
        var builder = DeckBuilder();

        // Act
        var deck = builder.CreateNewDeck("Test deck");

        if (deck == null || deck.Cards.Count == 0)
        {
            throw new InvalidOperationException("Deck is empty.");
        }

        // Assert deck can be created, has 52 cards and is named
        Assert.NotNull(deck);
        Assert.Equal(52, deck.Cards.Count);
        Assert.Equal("Test deck", deck.Name);
    }

    [Fact]
    public void TestCreateNewDeckWithUserId()
    {
        // Arrange
        var builder = DeckBuilder();

        // Act
        var deck = builder.CreateNewDeck("Test deck", 666);

        if (deck == null || deck.Cards.Count == 0)
        {
            throw new InvalidOperationException("Deck is empty.");
        }

        if (deck.UserId == null)
        {
            throw new InvalidOperationException("User ID is empty.");
        }

        // Assert deck can be created, has 52 cards, is named and has a user ID
        Assert.NotNull(deck);
        Assert.Equal(52, deck.Cards.Count);
        Assert.Equal("Test deck", deck.Name);
        Assert.Equal(666, deck.UserId);
    }

    [Fact]
    public void TestRenameDeck()
    {
        // Arrange
        var builder = DeckBuilder();
        var deck = builder.CreateNewDeck("Test deck");

        // Act
        var renamedDeck = builder.RenameDeck(deck.Id, "New name");

        // Assert deck was renamed
        Assert.NotNull(renamedDeck);
        Assert.Equal("New name", renamedDeck.Name);
    }

    [Fact]
    public void TestAddOrEditNotes()
    {
        // Arrange
        var builder = DeckBuilder();
        var deck = builder.CreateNewDeck("Notes deck");

        // Act
        var deckWithNotes = builder.AddOrEditNotes(deck.Id, "Notes added");

        // Assert deck has notes added
        Assert.NotNull(deckWithNotes);
        Assert.Equal("Notes added", deckWithNotes.Notes);
    }

    [Fact]
    public void TestShuffleDeck()
    {

        // Arrange
        var builder = DeckBuilder();
        var deck = builder.CreateNewDeck("Shuffle deck");

        // Act
        var shuffledDeck = builder.ShuffleDeck(deck.Id);

        // Assert deck has been shuffled
        Assert.NotNull(shuffledDeck);
        Assert.True(builder.IsDeckShuffled(shuffledDeck));
    }

    [Fact]
    public void TestReorderDeck()
    {
        var builder = DeckBuilder();
        var deck = builder.CreateNewDeck("Reorder deck");

        var currentOrder = deck.Select(c => c.Id).ToList();

        builder.ShuffleDeck(deck.Id);

        var shuffledOrder = deck.Select(c => c.Id).ToList();

        if (currentOrder.SequenceEqual(shuffledOrder))
        {
            throw new InvalidOperationException("Deck has not been shuffled.");
        }

        builder.ReorderDeck(deck.Id);

        var reorderedOrder = deck.Select(c => c.Id).ToList();

        if (!currentOrder.SequenceEqual(reorderedOrder))
        {
            throw new InvalidOperationException("Deck has not been reordered correctly.");
        }

        Assert.NotNull(deck);
        Assert.False(builder.IsDeckShuffled(deck));
    }

    [Fact]
    public void TestDrawOneCard()
    {
        var builder = DeckBuilder();
        var deck = builder.CreateNewDeck("Test draw one card");
        builder.ShuffleDeck(deck.Id);

        var card = builder.DrawOneCard(deck.Id);

        Assert.NotNull(card);
        Assert.IsType<Card>(card);
        Assert.InRange(card.OriginalPosition, 1, 52);
        Assert.Contains(card.Suit, new[] { "Hearts", "Spades", "Diamonds", "Clubs" });
    }

    [Fact]
    public void TestAddCardToReviewPile()
    {
        // Arrange
        var builder = DeckBuilder();
        var deck = builder.CreateNewDeck("Add 1 card to review");
        var shuffled = builder.ShuffleDeck(deck.Id);
        var card = builder.DrawOneCard(shuffled.Id);

        builder.AddCardToReviewPile(card);
        Console.WriteLine($"The drawn card is: {card.Rank} of {card.Suit}");

        // Act
        var returnedCard = builder.reviewPileService.GetNextCardFromReviewPile();
        Console.WriteLine($"The returnedCard card is: {returnedCard.Rank} of {returnedCard.Suit}");

        // Assert
        Assert.Equal(returnedCard, card);
    }

    [Fact]
    public void TestDrawThreeCardsForReview()
    {
        // Arrange
        var builder = DeckBuilder();
        var deck = builder.CreateNewDeck("Draw three cards from pile");
        var shuffled = builder.ShuffleDeck(deck.Id);
        var cards = new List<Card>();

        // Act
        // Draw three cards and add them to the review pile
        for (int i = 0; i < 3; i++)
        {
            var card = builder.DrawOneCard(shuffled.Id);
            builder.AddCardToReviewPile(card);
            cards.Add(card);
        }

        // Get the next three cards from the review pile
        var returnedCards = new List<Card>();
        for (int i = 0; i < 3; i++)
        {
            var card = builder.reviewPileService.GetNextCardFromReviewPile();
            returnedCards.Add(card);
        }

        // Assert
        Assert.Collection(returnedCards,
            card => Assert.Equal(card, returnedCards[0]),
            card => Assert.Equal(card, returnedCards[1]),
            card => Assert.Equal(card, returnedCards[2]));
    }

    [Fact]
    public void TestGetAllDecks()
    {
        // Arrange
        var builder = DeckBuilder();

        // Act
        var decks = builder.GetAllDecks();

        // Assert
        decks.ForEach(deck =>
        {
            Assert.NotNull(deck);
            Assert.NotNull(deck.Name);
        });
    }

    [Fact]
    public void TestGetDecksByUserId()
    {
        // Arrange
        var builder = DeckBuilder();
        var userId = 666;
        var deck = builder.CreateNewDeck("TestGetDecksByUserId", userId);

        // Act
        var decks = builder.GetDecksByUserId(deck.UserId);

        // Assert
        decks.ForEach(deck =>
        {
            Assert.NotNull(deck);
            Assert.NotNull(deck.Name);
            Assert.Equal(userId, deck.UserId);
        });
    }
}