namespace ProjectG.ClientService.Web.Areas.Product.Services
{
    using Bogus;

    using ProjectG.ClientService.Infrastructure.ProductApi.DTO;

    public sealed class ProductFaker : Faker<ProductWriteModel>
    {
        public ProductFaker()
        {
            this.RuleFor(product => product.Name, faker => faker.Commerce.ProductName());
            this.RuleFor(product => product.Description, faker => faker.Random.Words(faker.Random.Int(1, 40)));
            this.RuleFor(product => product.Price, faker => faker.Random.Float(1, 1000));
            this.RuleFor(product => product.Count, faker => faker.Random.Int(1, 1000));
        }
    }
}