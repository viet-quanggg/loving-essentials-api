using Microsoft.AspNetCore.Mvc;
using LovingEssentials.DataAccess.DTOs;
using LovingEssentials.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LovingEssentials.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _cartRepository;

        public CartController(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository ?? throw new ArgumentNullException(nameof(cartRepository));
        }

        // GET: api/cart
        [HttpGet]
        public async Task<IActionResult> GetAllCarts()
        {
            try
            {
                var carts = await _cartRepository.GetAllCartsAsync();
                return Ok(carts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/cart/{cartId}
        [HttpGet("{cartId}")]
        public async Task<IActionResult> GetCartById(int cartId)
        {
            try
            {
                var cart = await _cartRepository.GetCartByIdAsync(cartId);
                if (cart == null)
                {
                    return NotFound();
                }
                return Ok(cart);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("check/{userid}")]
        public async Task<IActionResult> GetCartofUser(int userid)
        {
            try
            {
                var cart = await _cartRepository.GetAllCartsofUserAsync(userid);
                if (cart == null)
                {
                    return NotFound();
                }
                return Ok(cart);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/cart/{buyerId}/{productId}/{quantity}
        [HttpPost("{buyerId}/{productId}/{quantity}")]
        public async Task<IActionResult> AddProductsToCart(int buyerId, int productId, int quantity)
        {
            try
            {
                var cart = await _cartRepository.AddProductsToCartAsync(buyerId, productId, quantity);
                return Ok(cart);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/cart/{buyerId}/{productId}/{quantity}
        [HttpDelete("{buyerId}/{productId}/{quantity}")]
        public async Task<IActionResult> RemoveProductFromCart(int buyerId, int productId, int quantity)
        {
            try
            {
                var cart = await _cartRepository.RemoveProductFromCartAsync(buyerId, productId, quantity);
                return Ok(cart);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpDelete("clear/{buyerId}/{productId}")]
        public async Task<IActionResult> ClearProductFromCart(int buyerId, int productId)
        {
            try
            {
                var cart = await _cartRepository.ClearProductFromCartAsync(buyerId, productId);
                return Ok(cart);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/cart/{cartId}
        [HttpDelete("{cartId}")]
        public async Task<IActionResult> DeleteCart(int cartId)
        {
            try
            {
                await _cartRepository.DeleteCartAsync(cartId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
