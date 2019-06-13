namespace ProjectG.OrderService.Core.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Newtonsoft.Json;

    public class OrderPosition
    {
        [Key, Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public long OrderId { get; set; }

        [Required]
        public long ProductId { get; set; }

        [Required]
        public int Count { get; set; }

        [Required]
        public double Price { get; set; }

        [JsonIgnore, ForeignKey(nameof(OrderPosition.OrderId))]
        public Order Order { get; set; }
    }
}