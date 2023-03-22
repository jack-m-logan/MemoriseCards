using System;
using System.ComponentModel.DataAnnotations;

namespace MemoriseCards.Models
{
    public class CardModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(10)]
        public string Suit { get; set; }

        [Required]
        public int Rank { get; set; }

        public float TotalCardScore { get; set; }

        public CardModel(int id, string suit, int rank)
        {
            Id = id;
            Suit = suit;
            Rank = rank;
        }
    }
}

