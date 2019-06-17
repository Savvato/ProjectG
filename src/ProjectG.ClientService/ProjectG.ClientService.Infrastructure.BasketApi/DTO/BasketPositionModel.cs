namespace ProjectG.ClientService.Infrastructure.BasketApi.DTO
{
    public class BasketPositionModel
    {
        public long Id { get; set; }

        public long CustomerId { get; set; }

        public long ProductId { get; set; }

        public int Quantity { get; set; }

        public float Price { get; set; }
    }
}