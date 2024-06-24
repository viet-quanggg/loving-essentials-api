using LovingEssentials.DataAccess.DTOs;
using LovingEssentials.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LovingEssentials.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        [HttpGet]
        public async Task<ActionResult<List<OrderDTO>>> GetOrders()
        {
            var result = await _orderRepository.GetOrders();
            return result;
        }
    }
}
