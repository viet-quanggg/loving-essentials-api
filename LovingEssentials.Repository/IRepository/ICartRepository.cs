using LovingEssentials.DataAccess.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LovingEssentials.Repository.IRepository
{
    public interface ICartRepository
    {
        Task<List<CartDTO>> GetAllCartsAsync();
        Task DeleteCartAsync(int cartId);
        Task<CartDTO> AddProductsToCartAsync(int buyerId, int productId, int quantity);
        Task<CartDTO> RemoveProductFromCartAsync(int buyerId, int productId, int quantity);
        Task<CartDTO> GetCartByIdAsync(int cartId);
        Task<List<CartDTO>> GetAllCartsofUserAsync(int userid);
        Task<CartDTO> ClearProductFromCartAsync(int buyerId, int productId);
    }
}
