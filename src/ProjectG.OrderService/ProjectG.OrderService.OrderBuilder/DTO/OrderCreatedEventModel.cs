namespace ProjectG.OrderService.OrderBuilder.DTO
{
    public class OrderCreatedEventModel
    {
        public long OrderId { get; set; }

        public long CustomerId { get; set; }
    }
}