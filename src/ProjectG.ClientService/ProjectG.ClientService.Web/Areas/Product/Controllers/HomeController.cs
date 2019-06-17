namespace ProjectG.ClientService.Web.Areas.Product.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using ProjectG.ClientService.Infrastructure.Interfaces;
    using ProjectG.ClientService.Infrastructure.ProductApi.DTO;

    [Area("Product")]
    [Route("[area]/[action]")]
    public class HomeController : Controller
    {
        private readonly IProductRepository productRepository;

        public HomeController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<ProductModel> products = await this.productRepository.Get();
            return this.View(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> View(long id)
        {
            ProductModel product = await this.productRepository.Get(id);
            return this.View(product);
        }
    }
}