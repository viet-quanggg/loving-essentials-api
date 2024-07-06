using LovingEssentials.DataAccess.DTOs.Payment;
using LovingEssentials.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace LovingEssentials.API.Controllers
{
    public class PaymentController : ControllerBase
    {

        private readonly IPaymentRepository _paymentRepository;

        public PaymentController(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }
        
        [HttpPost("/create-payment-link-PayOs")]
        public async Task<IActionResult> GetPayOsPaymentLink([FromQuery] CreatePaymentLinkRequest request)
        {
            try
            {
                var result = await _paymentRepository.CreatePaymentLink(request);
                
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [HttpGet("/GetPayOsOrder/{orderId}")]
        public async Task<IActionResult> GetOrder([FromRoute] int orderId)
        {
            try
            {
                var result = await _paymentRepository.GetOrder(orderId);
                return Ok(result);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }

        }

    }
}