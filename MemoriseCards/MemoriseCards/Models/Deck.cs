using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MemoriseCards.Models
{
    public class Deck
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        public List<Card> Cards { get; set; }

        [StringLength(255)]
        public string Notes { get; set; }

        [Required]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        public Deck()
        {
            Cards = new List<Card>();
        }

        //public Deck(int id, string name, int userId, string notes = null)
        //{
        //    Id = id;
        //    Name = name;
        //    UserId = userId;
        //    Notes = notes;
        //}
    }
}