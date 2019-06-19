namespace ProjectG.ClientService.Infrastructure.BasketApi.DTO
{
    public class BasketPositionWriteModel
    {
        public long CustomerId { get; set; }

        public long ProductId { get; set; }

        public int Quantity { get; set; }

        public float Price { get; set; }
    }
}