using LovingEssentials.BusinessObject;
using LovingEssentials.DataAccess.DTOs;
using LovingEssentials.DataAccess.DTOs.Shipper;
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
        [HttpGet("ByShipper/{shipperId}")]
        public async Task<ActionResult<List<OrderResponse>>> GetOrdersByShipperId(int shipperId, [FromQuery] OrderStatus? status = null, [FromQuery] string buyerName = null, [FromQuery] string productName = null)
        {
            try
            {
                var orders = await _orderRepository.GetOrdersByShipperId(shipperId, status, buyerName);

                if (orders == null || orders.Count == 0)
                {
                    return NotFound("No orders found for the given shipper ID.");
                }

                return Ok(orders);
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving data from the database: {ex.Message}");
            }
        }

        [HttpPut("status")]
        public async Task<IActionResult> UpdateOrderStatus([FromBody] UpdateStatusRequest request)
        {

            try
            {
                var result = await _orderRepository.UpdateOrderStatusByShipper(request);

                if (!result)
                {
                    return NotFound($"Order with ID {request.orderId} not found or shipper ID does not match.");
                }

                return Ok("Order status updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating order status: {ex.Message}");
            }
        }
    }
}
