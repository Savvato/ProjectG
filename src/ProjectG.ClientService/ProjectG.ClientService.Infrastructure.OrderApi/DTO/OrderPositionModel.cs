namespace ProjectG.ClientService.Infrastructure.OrderApi.DTO
{
    public class OrderPositionModel
    {
        public long Id { get; set; }

        public long OrderId { get; set; }

        public long ProductId { get; set; }

        public string ProductName { get; set; }

        public string ProductDescription { get; set; }

        public int Count { get; set; }

        public double Price { get; set; }
    }
}