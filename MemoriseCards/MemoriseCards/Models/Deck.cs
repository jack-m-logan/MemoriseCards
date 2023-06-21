using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MemoriseCards.Models
{
    public class Deck
    {
        [Key]
        public int Id { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        public List<Card> Cards { get; set; }

        [StringLength(255)]
        public string? Notes { get; set; }

        public int? UserId { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }

        public Deck(string name)
        {
            Cards = new List<Card>();
            Name = name;
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
    }
}