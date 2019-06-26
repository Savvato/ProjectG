namespace ProjectG.BasketService.Api.DTO
{
    public class OrderCreatedEventModel
    {
        public long OrderId { get; set; }

        public long CustomerId { get; set; }
    }
}