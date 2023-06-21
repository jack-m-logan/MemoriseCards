using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MemoriseCards.Models
{
    public class POA
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int CardId { get; set; }

        [Required]
        [StringLength(255)]
        public string Person { get; set; }

        [Required]
        [StringLength(255)]
        public string Object { get; set; }

        [Required]
        [StringLength(255)]
        public string Action { get; set; }

        [ForeignKey("CardId")]
        public Card Card { get; set; }

        public POA() { }

        public POA(int cardId, string person, string Object, string action)
        {
            CardId = cardId;
            Person = person;
            this.Object = Object;
            Action = action;
        }
    }
}