using AutoMapper;
using AutoMapper.QueryableExtensions;
using LovingEssentials.BusinessObject;
using LovingEssentials.DataAccess.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LovingEssentials.DataAccess.DAOs
{
    public class CartDAO
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CartDAO(DataContext dataContext, IMapper mapper)
        {
            _context = dataContext;
            _mapper = mapper;
        }

        public async Task<List<CartDTO>> GetCarts()
        {
            try
            {
                var result = await _context.Carts
                    .ProjectTo<CartDTO>(_mapper.ConfigurationProvider)
                    .ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<CartDTO> GetCartByIdAsync(int cartId)
        {
            try
            {
                var cart = await _context.Carts
                    .ProjectTo<CartDTO>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(c => c.Id == cartId);

                if (cart == null)
                    throw new Exception($"Cart with ID {cartId} not found");

                return cart;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving cart with ID {cartId}", ex);
            }
        }

        public async Task<CartDTO> AddProductsToCart(int buyerId, int productId, int quantity)
        {
            try
            {
                // Check if there's an existing cart for the buyer
                var cart = await _context.Carts
                    .Include(c => c.Products)
                    .FirstOrDefaultAsync(c => c.BuyerId == buyerId);

                if (cart == null)
                {
                    // Create a new cart if none exists for the buyer
                    cart = new Cart
                    {
                        CreateAt = DateTime.Now,
                        UpdateAt = DateTime.Now,
                        BuyerId = buyerId,
                        Products = new Dictionary<Product, int>()
                    };
                    _context.Carts.Add(cart);
                    await _context.SaveChangesAsync();
                }

                // Fetch the product
                var product = await _context.Products.FindAsync(productId);
                if (product == null)
                {
                    throw new Exception($"Product with ID {productId} not found");
                }

                // Add or update the product quantity in the cart
                if (cart.Products.ContainsKey(product))
                {
                    cart.Products[product] += quantity;
                }
                else
                {
                    cart.Products.Add(product, quantity);
                }

                // Update the price of the cart
                cart.Price += product.Price * quantity;

                // Update timestamp of the cart
                cart.UpdateAt = DateTime.Now;
                

                await _context.SaveChangesAsync();

                // Map to CartDTO
                var cartDto = _mapper.Map<CartDTO>(cart);

                // Optionally, populate the ProductDTO objects in the cart DTO
                foreach (var kvp in cartDto.Products.ToList())
                {
                    var productDto = await _context.Products
                        .Where(p => p.Id == kvp.Key.Id)
                        .ProjectTo<ProductDTO>(_mapper.ConfigurationProvider)
                        .FirstOrDefaultAsync();

                    if (productDto != null)
                    {
                        cartDto.Products[productDto] = kvp.Value;
                    }
                }

                return cartDto;
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding products to cart", ex);
            }
        }

        public async Task<CartDTO> RemoveProductFromCart(int buyerId, int productId, int quantity)
        {
            try
            {
                // Retrieve the cart for the buyer
                var cart = await _context.Carts
                    .Include(c => c.Products)
                    .FirstOrDefaultAsync(c => c.BuyerId == buyerId);

                if (cart == null)
                {
                    throw new Exception("Cart not found");
                }

                // Fetch the product
                var product = await _context.Products.FindAsync(productId);
                if (product == null)
                {
                    throw new Exception($"Product with ID {productId} not found");
                }

                // Check if the product exists in the cart
                if (!cart.Products.ContainsKey(product))
                {
                    throw new Exception($"Product with ID {productId} not found in the cart");
                }

                // Remove the product from the cart or decrease its quantity
                if (cart.Products[product] > quantity)
                {
                    cart.Products[product] -= quantity;
                }
                else
                {
                    cart.Products.Remove(product);
                }

                // Update the price of the cart
                cart.Price -= product.Price * quantity;

                // Update timestamp of the cart
                cart.UpdateAt = DateTime.Now;

                await _context.SaveChangesAsync();

                // Map to CartDTO
                var cartDto = _mapper.Map<CartDTO>(cart);

                // Optionally, populate the ProductDTO objects in the cart DTO
                foreach (var kvp in cartDto.Products.ToList())
                {
                    var productDto = await _context.Products
                        .Where(p => p.Id == kvp.Key.Id)
                        .ProjectTo<ProductDTO>(_mapper.ConfigurationProvider)
                        .FirstOrDefaultAsync();

                    if (productDto != null)
                    {
                        cartDto.Products[productDto] = kvp.Value;
                    }
                }

                return cartDto;
            }
            catch (Exception ex)
            {
                throw new Exception("Error removing product from cart", ex);
            }
        }
        public async Task DeleteCart(int cartId)
        {
            try
            {
                var cart = await _context.Carts.FindAsync(cartId);
                if (cart != null)
                {
                    _context.Carts.Remove(cart);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new Exception($"Cart with ID {cartId} not found");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting cart", ex);
            }
        }
    }
}

