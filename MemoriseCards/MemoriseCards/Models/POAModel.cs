using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MemoriseCards.Models
{
    public class POAModel
    {
        [Key]
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

        [ForeignKey("CardID")]
        public CardModel Card { get; set; }

        public POAModel() { }

        public POAModel(int id, int cardId, string person, string Object, string action)
        {
            Id = id;
            CardId = cardId;
            Person = person;
            this.Object = Object;
            Action = action;
        }
    }
}