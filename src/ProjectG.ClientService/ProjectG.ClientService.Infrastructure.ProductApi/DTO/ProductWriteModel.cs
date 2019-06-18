namespace ProjectG.ClientService.Infrastructure.ProductApi.DTO
{
    public class ProductWriteModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int Count { get; set; } = 0;

        public float Price { get; set; }
    }
}