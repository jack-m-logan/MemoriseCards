using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MemoriseCards.Models
{
	public class Round
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public int DeckId { get; set; }

        [Required]
        public int RoundNumber { get; set; }

        [ForeignKey("DeckId")]
        public Deck Deck { get; set; }

        public ICollection<Score> Scores { get; set; }

        public Round()
		{
		}

		public Round(int id, int deckId, int roundNumber)
		{
			Id = id;
			DeckId = deckId;
			RoundNumber = roundNumber;
		}
	}
}