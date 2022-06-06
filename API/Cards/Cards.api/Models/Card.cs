using System.ComponentModel.DataAnnotations;

namespace Cards.api.Models
{
    public class Card
    {
        [Key]
        public Guid Id { get; set; }
        public string? CardHolderName { get; set; }
        public int CardNumber { get; set; }
        public int ExpiryMonth { get; set; }
        public int ExpiryYear { get; set; }
        public int CVV { get; set; }
    }
}
