namespace ProjectG.ClientService.Web.Areas.Customer.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using ProjectG.ClientService.Infrastructure.CustomerApi.DTO;
    using ProjectG.ClientService.Infrastructure.DTO;
    using ProjectG.ClientService.Infrastructure.Interfaces;

    [Area("Customer")]
    [Route("[area]/[action]")]
    public class HomeController : Controller
    {
        private readonly ICustomerRepository customerRepository;

        public HomeController(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
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
    }
}