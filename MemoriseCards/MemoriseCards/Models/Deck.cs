using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MemoriseCards.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace MemoriseCards.Models
{
    public class Deck
    {
        [Key]
        public int Id { get; set; }

        [StringLength(255)]
        [Required(ErrorMessage = "Deck name is required.")]
        public string Name { get; set; }

        public List<Card> Cards { get; set; }

        [StringLength(255)]
        public string? Notes { get; set; }

        public int? UserId { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }

        private readonly MemoriseCardsDbContext _context;

        public Deck(string name, int? userId, MemoriseCardsDbContext context)
        {
            Cards = new List<Card>();
            Name = name;
            UserId = userId;
            _context = context;
            User = GetUserFromUserId(context);
        }

        public Deck(string name)
        {
            Cards = new List<Card>();
            Name = name;
        }

        public Deck()
        {
        }

        public List<int> Select(Func<Card, int> selector)
        {
            List<int> result = new List<int>();
            foreach (var card in Cards)
            {
                result.Add(selector(card));
            }
            return result;
        }

        private User? GetUserFromUserId(MemoriseCardsDbContext context)
        {
            if (UserId.HasValue)
            {
                return context.User.Find(UserId.Value);
            }

            return null;
        }

        //private void LoadCards()
        //{
        //    Cards = _context.Card.Where(card => card.DeckId == Id).ToList();
        //}
    }
}