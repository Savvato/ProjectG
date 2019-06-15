namespace ProjectG.BasketService.Api.DTO
{
    using System.ComponentModel.DataAnnotations;

    public class BasketPositionCreationModel
    {
        [Required]
        public long CustomerId { get; set; }

        [Required]
        public long ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public float Price { get; set; }
    }
}