using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MemoriseCards.Models
{
	public class RoundModel
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public int DeckId { get; set; }

        [Required]
        public int RoundNumber { get; set; }

        [ForeignKey("DeckId")]
        public DeckModel Deck { get; set; }

        public ICollection<ScoreModel> Scores { get; set; }

        public RoundModel()
		{
		}

		public RoundModel(int id, int deckId, int roundNumber)
		{
			Id = id;
			DeckId = deckId;
			RoundNumber = roundNumber;
		}
	}
}