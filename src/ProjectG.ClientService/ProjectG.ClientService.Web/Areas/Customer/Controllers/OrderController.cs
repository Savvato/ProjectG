namespace ProjectG.ClientService.Web.Areas.Customer.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using ProjectG.ClientService.Infrastructure.Interfaces;
    using ProjectG.ClientService.Infrastructure.OrderApi.DTO;

    [Area("Customer")]
    [Route("[area]/{customerId}/[controller]/[action]")]
    public class OrderController : Controller
    {
        private readonly IOrderRepository orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index(long customerId)
        {
            IEnumerable<OrderModel> orders = await this.orderRepository.GetByCustomerId(customerId);
            return this.View(orders);
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> View(long customerId, long orderId)
        {
            OrderModel order = await this.orderRepository.Get(orderId);
            return this.View(order);
        }

        [HttpPost]
        public async Task<IActionResult> Create(int customerId)
        {
            await this.orderRepository.Create(customerId);
            return this.RedirectToAction(actionName: "Index", controllerName: "Order", routeValues: new { customerId = customerId });
        }

        [HttpPost("{orderId}")]
        public async Task<IActionResult> Status(long customerId, long orderId, [FromForm] string status)
        {
            await this.orderRepository.UpdateStatus(orderId, status);
            return this.RedirectToAction("View", new { customerId = customerId, orderId = orderId });
        }
    }
}