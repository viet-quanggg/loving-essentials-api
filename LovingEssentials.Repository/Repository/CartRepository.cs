using LovingEssentials.DataAccess.DAOs;
using LovingEssentials.DataAccess.DTOs;
using LovingEssentials.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LovingEssentials.Repository.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly CartDAO _cartDAO;

        public CartRepository(CartDAO cartDAO)
        {
            _cartDAO = cartDAO;
        }

        public async Task<List<CartDTO>> GetAllCartsAsync()
        {
            try
            {
                return await _cartDAO.GetCarts();
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving carts from repository", ex);
            }
        }
        public async Task DeleteCartAsync(int cartId)
        {
            try
            {
                await _cartDAO.DeleteCart(cartId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting cart with ID {cartId} from repository", ex);
            }
        }

        public async Task<CartDTO> AddProductsToCartAsync(int buyerId, int productId, int quantity)
        {
            try
            {
                return await _cartDAO.AddProductsToCart(buyerId, productId, quantity);
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding products to cart in repository", ex);
            }
        }

        public async Task<CartDTO> RemoveProductFromCartAsync(int buyerId, int productId, int quantity)
        {
            try
            {
                return await _cartDAO.RemoveProductFromCart(buyerId, productId, quantity);
            }
            catch (Exception ex)
            {
                throw new Exception("Error removing product from cart in repository", ex);
            }
        }
        public async Task<CartDTO> GetCartByIdAsync(int cartId)
        {
            try
            {
                return await _cartDAO.GetCartByIdAsync(cartId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving cart with ID {cartId} from repository", ex);
            }
        }

        public async Task<List<CartDTO>> GetAllCartsofUserAsync(int userid)
        {
            try
            {
                return await _cartDAO.GetCartsofUser(userid);
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving carts from repository", ex);
            }
        }
    }
}
