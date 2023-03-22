using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MemoriseCards.Models
{
    public class DeckModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Notes { get; set; }

        [Required]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public UserModel User { get; set; }

        public DeckModel() { }

        public DeckModel(int id, string name, int userId, string notes = null)
        {
            Id = id;
            Name = name;
            UserId = userId;
            Notes = notes;
        }
    }
}