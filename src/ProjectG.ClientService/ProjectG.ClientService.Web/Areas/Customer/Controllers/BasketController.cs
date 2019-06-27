namespace ProjectG.ClientService.Web.Areas.Customer.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Bogus;

    using Microsoft.AspNetCore.Mvc;

    using ProjectG.ClientService.Infrastructure.BasketApi.DTO;
    using ProjectG.ClientService.Infrastructure.DTO;
    using ProjectG.ClientService.Infrastructure.Interfaces;
    using ProjectG.ClientService.Infrastructure.ProductApi.DTO;

    [Area("Customer")]
    [Route("[area]/{id}/[controller]/[action]")]
    public class BasketController : Controller
    {
        private readonly ICustomerRepository customerRepository;
        private readonly IProductRepository productRepository;
        private readonly IBasketRepository basketRepository;

        public BasketController(ICustomerRepository customerRepository, IProductRepository productRepository, IBasketRepository basketRepository)
        {
            this.customerRepository = customerRepository;
            this.productRepository = productRepository;
            this.basketRepository = basketRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Generate([FromRoute] long id, [FromForm] int count)
        {
            CustomerDetailedModel customer = await this.customerRepository.Get(id);
            IEnumerable<BasketPositionModel> basketPositions = customer.Basket;
            IEnumerable<ProductModel> products = await this.productRepository.Get();

            IEnumerable<ProductModel> availableProducts =
                products.Where(product => basketPositions.All(position => position.ProductId != product.Id))
                    .ToList();
            int availableCount = availableProducts.Count() > count ? count : availableProducts.Count();
            Faker faker = new Faker();
            IEnumerable<ProductModel> productsForBasket = faker.PickRandom(availableProducts, availableCount);
            IEnumerable<BasketPositionWriteModel> newBasketPositions = productsForBasket.Select(product => new BasketPositionWriteModel
            {
                CustomerId = id,
                ProductId = product.Id,
                Price = product.Price,
                Quantity = faker.Random.Int(1, product.Count)
            });

            await Task.WhenAll(newBasketPositions.Select(this.basketRepository.Create));

            return this.RedirectToAction(actionName: "View", controllerName: "Home", routeValues: new { id = id });
        }
    }
}