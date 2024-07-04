using LovingEssentials.DataAccess.DTOs;
using LovingEssentials.Repository.IRepository;
using LovingEssentials.Repository.Repository;
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
        [HttpGet("detail")]
        public async Task<ActionResult<List<OrderDTO>>> GetProductbyId(int Id)
        {
            var result = await _orderRepository.GetOrdersByUserId(Id);
            return result;
        }
        [HttpGet("order-detail")]
        public async Task<ActionResult<List<OrderDetailDTO>>> GetOrderDetailbyId(int orderid)
        {
            var result = await _orderRepository.GetOrderDetailsById(orderid);
            return result;
        }
    }
}
