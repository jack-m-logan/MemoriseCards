using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MemoriseCards.Models
{
	public class ScoreModel
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public int RoundId { get; set; }

        [Required]
        public int UserId { get; set; }

		[Required]
		public DateTime Date { get; set; }

        public float Ratio { get; set; }

		public int TotalScore { get; set; }

		[ForeignKey("RoundId")]
		public RoundModel Round { get; set; }

		[ForeignKey("UserId")]
		public UserModel User { get; set; }

		public ScoreModel()
		{
		}

		public ScoreModel(int id, int roundId, DateTime date)
		{
			Id = id;
			RoundId = roundId;
			Date = date;
		}
	}
}