namespace ProjectG.OrderService.WriteApi.DTO
{
    using System.ComponentModel.DataAnnotations;

    public class OrderCreationModel
    {
        [Required]
        public long CustomerId { get; set; }
    }
}
