namespace ProjectG.ClientService.Web.Areas.Customer.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using ProjectG.ClientService.Infrastructure.Interfaces;

    [Area("Customer")]
    [Route("[area]/{id}/[controller]/[action]")]
    public class OrderController : Controller
    {
        private readonly IOrderRepository orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Create(int id)
        {
            await this.orderRepository.Create(id);
            return this.RedirectToAction(actionName: "View", controllerName: "Home", routeValues: new { id = id });
        }
    }
}