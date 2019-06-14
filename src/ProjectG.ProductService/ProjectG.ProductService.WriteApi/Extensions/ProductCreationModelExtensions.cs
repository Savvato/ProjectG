namespace ProjectG.ProductService.WriteApi.Extensions
{
    using ProjectG.ProductService.Core.Models;
    using ProjectG.ProductService.WriteApi.DTO;

    public static class ProductCreationModelExtensions
    {
        public static Product ToProduct(this ProductCreationModel productCreationModel)
        {
            return new Product
            {
                Name = productCreationModel.Name,
                Description = productCreationModel.Description,
                Count = productCreationModel.Count,
                Price = productCreationModel.Price
            };
        }
    }
}
