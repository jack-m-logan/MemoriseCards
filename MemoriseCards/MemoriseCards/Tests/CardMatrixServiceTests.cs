using System;
using MemoriseCards.Data;
using MemoriseCards.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Xunit;

namespace MemoriseCards.Tests
{
    public class CardMatrixServiceTests
    {
        private MemoriseCardsDbContext _context;
        private CardMatrixService _matrixService;
        private DeckBuilder _deckBuilder;

        public CardMatrixServiceTests()
        {
            var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

            var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");

            var options = new DbContextOptionsBuilder<MemoriseCardsDbContext>()
            .UseNpgsql(connectionString)
            .Options;

            _context = new MemoriseCardsDbContext(options, configuration);
            _matrixService = new CardMatrixService(_context);
            _deckBuilder = new DeckBuilder(_context);
        }

        [Fact]
        public void TestCreatePOA()
        {
            // Arrange
            var person = "Person";
            var obj = "Object";
            var action = "Action";

            var deck = _deckBuilder.CreateNewDeck("TestCreatePOA");
            var card = deck.Cards.First();
            _context.SaveChanges();

            // Act
            var createdPOA = _matrixService.CreatePOA(card.Id, person, obj, action);

            // Assert
            Assert.NotNull(createdPOA);
            Assert.Equal(card.Id, createdPOA.CardId);
            Assert.Equal(person, createdPOA.Person);
            Assert.Equal(obj, createdPOA.Object);
            Assert.Equal(action, createdPOA.Action);
        }

        [Fact]
        public void TestAddPOAToCard()
        {
            // Arrange
            var deck = _deckBuilder.CreateNewDeck("TestCreatePOA");
            var card = deck.Cards.First();
            _context.SaveChanges();

            // Act
            var poa = new POA(card.Id, "Person", "Object", "Action");
            _matrixService.AddPOAToCard(card, poa);

            // Assert
            Assert.Equal(poa, card.POA);
        }

        [Fact]
        public void TestDeletePOA()
        {
            // Arrange
            var deck = _deckBuilder.CreateNewDeck("TestCreatePOA");
            var card = deck.Cards.First();
            _context.SaveChanges();

            var poa = new POA(card.Id, "Person1", "Object1", "Action1");
            _matrixService.AddPOAToCard(card, poa);

            // Act
            _matrixService.DeletePOA(card);

            // Assert
            Assert.Null(card.POA);
        }

        [Fact]
        public void TestUpdatePOAPerson()
        {
            // Arrange
            var deck = _deckBuilder.CreateNewDeck("TestCreatePOA");
            var card = deck.Cards.First();
            _context.SaveChanges();

            var originalPOA = new POA(card.Id, "OG Person", "OG Object", "OG Action");
            _matrixService.AddPOAToCard(card, originalPOA);

            var newValue = "Nu Person";

            // Act
            _matrixService.UpdatePOAPerson(card, newValue);

            // Assert
            Assert.NotNull(card.POA);
            Assert.Equal("Nu Person", card.POA.Person);
        }

        [Fact]
        public void TestUpdatePOAObject()
        {
            // Arrange
            var deck = _deckBuilder.CreateNewDeck("TestCreatePOA");
            var card = deck.Cards.First();
            _context.SaveChanges();

            var originalPOA = new POA(card.Id, "OG Person", "OG Object", "OG Action");
            _matrixService.AddPOAToCard(card, originalPOA);

            var newValue = "Nu Object";

            // Act
            _matrixService.UpdatePOAObject(card, newValue);

            // Assert
            Assert.NotNull(card.POA);
            Assert.Equal("Nu Object", card.POA.Object);
        }

        [Fact]
        public void TestUpdatePOAAction()
        {
            // Arrange
            var deck = _deckBuilder.CreateNewDeck("TestCreatePOA");
            var card = deck.Cards.First();
            _context.SaveChanges();

            var originalPOA = new POA(card.Id, "OG Person", "OG Object", "OG Action");
            _matrixService.AddPOAToCard(card, originalPOA);

            var newValue = "Nu Action";

            // Act
            _matrixService.UpdatePOAAction(card, newValue);

            // Assert
            Assert.NotNull(card.POA);
            Assert.Equal("Nu Action", card.POA.Action);
        }
    }
}