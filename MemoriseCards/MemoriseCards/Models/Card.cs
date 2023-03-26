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

        public float TotalCardScore { get; set; }

        public Card(int id, string suit, string rank)
        {
            Id = id;
            Suit = suit;
            Rank = rank;
        }
    }
}

