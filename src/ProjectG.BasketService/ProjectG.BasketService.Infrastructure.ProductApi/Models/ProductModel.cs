namespace ProjectG.BasketService.Infrastructure.ProductApi.Models
{
    public class ProductModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Count { get; set; }

        public float Price { get; set; }
    }
}