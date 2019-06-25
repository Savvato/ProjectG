namespace ProjectG.OrderService.WriteApi.DTO
{
    using System.ComponentModel.DataAnnotations;

    public class OrderInitModel
    {
        [Required]
        public long CustomerId { get; set; }
    }
}
