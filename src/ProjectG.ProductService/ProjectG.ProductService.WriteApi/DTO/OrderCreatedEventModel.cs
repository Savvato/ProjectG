namespace ProjectG.ProductService.WriteApi.DTO
{
    public class OrderCreatedEventModel
    {
        public long OrderId { get; set; }

        public long CustomerId { get; set; }
    }
}