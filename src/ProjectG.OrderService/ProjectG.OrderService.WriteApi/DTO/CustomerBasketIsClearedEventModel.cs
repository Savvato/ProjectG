namespace ProjectG.OrderService.WriteApi.DTO
{
    public class CustomerBasketIsClearedEventModel
    {
        public long CustomerId { get; set; }

        public long OrderId { get; set; }
    }
}