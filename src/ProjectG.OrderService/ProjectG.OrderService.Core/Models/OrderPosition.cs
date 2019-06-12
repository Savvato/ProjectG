namespace ProjectG.OrderService.Core.Models
{
    public class OrderPosition
    {
        public long Id { get; set; }

        public long OrderId { get; set; }

        public long ProductId { get; set; }

        public int Count { get; set; }

        public double Price { get; set; }
    }
}