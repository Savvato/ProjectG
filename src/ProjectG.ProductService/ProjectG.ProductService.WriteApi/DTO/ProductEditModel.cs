namespace ProjectG.ProductService.WriteApi.DTO
{
    public class ProductEditModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Count { get; set; }

        public float Price { get; set; }
    }
}