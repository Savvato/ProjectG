namespace ProjectG.ClientService.Web.Areas.Product.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using ProjectG.ClientService.Infrastructure.Interfaces;
    using ProjectG.ClientService.Infrastructure.ProductApi.DTO;
    using ProjectG.ClientService.Web.Areas.Product.Services;

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

        [HttpPost]
        public async Task<IActionResult> Edit([FromForm] ProductModel productWriteModel)
        {
            await this.productRepository.Edit(productWriteModel);
            return this.RedirectToAction("View", new {id = productWriteModel.Id});
        }

        [HttpPost]
        public async Task<IActionResult> Generate([FromForm] int count)
        {
            ProductFaker productFaker = new ProductFaker();
            IEnumerable<ProductWriteModel> generatedProducts = productFaker.Generate(count);

            await Task.WhenAll(
                generatedProducts.Select(this.productRepository.Create));

            return this.RedirectToAction("Index");
        }
    }
}