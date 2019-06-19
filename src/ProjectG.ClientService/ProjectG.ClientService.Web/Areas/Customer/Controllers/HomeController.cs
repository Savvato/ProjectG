namespace ProjectG.ClientService.Web.Areas.Customer.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Bogus;

    using Microsoft.AspNetCore.Mvc;

    using ProjectG.ClientService.Infrastructure.BasketApi.DTO;
    using ProjectG.ClientService.Infrastructure.CustomerApi.DTO;
    using ProjectG.ClientService.Infrastructure.DTO;
    using ProjectG.ClientService.Infrastructure.Interfaces;
    using ProjectG.ClientService.Infrastructure.ProductApi.DTO;
    using ProjectG.ClientService.Web.Areas.Customer.Services;

    [Area("Customer")]
    [Route("[area]/[action]")]
    public class HomeController : Controller
    {
        private readonly ICustomerRepository customerRepository;
        private readonly IProductRepository productRepository;
        private readonly IBasketRepository basketRepository;

        public HomeController(
            ICustomerRepository customerRepository, 
            IProductRepository productRepository, 
            IBasketRepository basketRepository)
        {
            this.customerRepository = customerRepository;
            this.productRepository = productRepository;
            this.basketRepository = basketRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<CustomerModel> customers = await this.customerRepository.Get();

            return this.View(customers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> View(long id)
        {
            CustomerDetailedModel customer = await this.customerRepository.Get(id);

            return this.View(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Generate([FromForm] int count)
        {
            CustomerFaker customerFaker = new CustomerFaker();
            IEnumerable<CustomerWriteModel> generatedCustomers = customerFaker.Generate(count);

            await Task.WhenAll(generatedCustomers.Select(this.customerRepository.Create));

            return this.RedirectToAction("Index");
        }

        [HttpPost("{id}/basket/generate")]
        public async Task<IActionResult> GenerateBasket([FromRoute] long id, [FromForm] int count)
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

            return this.RedirectToAction("View", new { id = id });
        }
    }
}