using System;
using System.ComponentModel.DataAnnotations;

namespace MemoriseCards.Models
{
    public class Card
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(10)]
        public string Suit { get; set; }

        [Required]
        [StringLength(10)]
        public string Rank { get; set; }

        public POA? POA { get; set; }

        public int? POAId { get; set; } 

        public float TotalCardScore { get; set; }

        public int OriginalPosition { get; set; }

        public Card(string suit, string rank)
        {
            Suit = suit;
            Rank = rank;
        }
    }
}

