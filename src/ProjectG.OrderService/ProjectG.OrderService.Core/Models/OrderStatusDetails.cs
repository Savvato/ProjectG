namespace ProjectG.OrderService.Core.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Newtonsoft.Json;

    public class OrderStatusDetails
    {
        [Key, Required, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long OrderId { get; set; }

        [Required]
        public bool IsBasketCleared { get; set; } = false;

        [Required]
        public bool AreProductsReserved { get; set; } = false;

        [JsonIgnore, ForeignKey(nameof(OrderStatusDetails.OrderId))]
        public Order Order { get; set; }

        [NotMapped]
        public bool IsReadyForPayments => this.IsBasketCleared && this.AreProductsReserved;
    }
}