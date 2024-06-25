using AutoMapper;
using AutoMapper.QueryableExtensions;
using LovingEssentials.BusinessObject;
using LovingEssentials.DataAccess.DTOs;
using LovingEssentials.DataAccess.Migrations;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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
                var carts = await _context.Carts.ToListAsync();
                var list = new List<CartDTO>();
                foreach (var cart in carts)
                {
                    var productsJson = cart.ProductsJson;
                    var productsDict = JsonConvert.DeserializeObject<Dictionary<int, int>>(productsJson);

                    var cartDto = _mapper.Map<CartDTO>(cart);
                    double totalPrice = 0;

                    foreach (var kvp in productsDict)
                    {
                        var productDto = await _context.Products
                            .Where(p => p.Id == kvp.Key)
                            .ProjectTo<ProductDTO>(_mapper.ConfigurationProvider)
                            .FirstOrDefaultAsync();

                        if (productDto != null)
                        {
                            double productPrice = productDto.Price;
                            int quantity = kvp.Value;
                            double productTotalPrice = productPrice * quantity;
                            totalPrice += productTotalPrice;

                            cartDto.Products.Add(kvp.Value, productDto);
                        }
                    }
                    cartDto.Price = totalPrice;
                    list.Add(cartDto);
                }
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<CartDTO>> GetCartsofUser(int userid)
        {
            try
            {
                var carts = await _context.Carts.Where(c => c.BuyerId == userid).ToListAsync();
                var list = new List<CartDTO>();
                foreach (var cart in carts)
                {
                    var productsJson = cart.ProductsJson;
                    var productsDict = JsonConvert.DeserializeObject<Dictionary<int, int>>(productsJson);

                    var cartDto = _mapper.Map<CartDTO>(cart);
                    double totalPrice = 0;

                    foreach (var kvp in productsDict)
                    {
                        var productDto = await _context.Products
                            .Where(p => p.Id == kvp.Key)
                            .ProjectTo<ProductDTO>(_mapper.ConfigurationProvider)
                            .FirstOrDefaultAsync();

                        if (productDto != null)
                        {
                            double productPrice = productDto.Price;
                            int quantity = kvp.Value;
                            double productTotalPrice = productPrice * quantity;
                            totalPrice += productTotalPrice;

                            cartDto.Products.Add(kvp.Value, productDto);
                        }
                    }
                    cartDto.Price = totalPrice;
                    list.Add(cartDto);
                }
                return list;
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
                    .FirstOrDefaultAsync(c => c.BuyerId == buyerId);

                if (cart == null)
                {
                    // Create a new cart if none exists for the buyer
                    cart = new Cart
                    {
                        CreateAt = DateTime.Now,
                        UpdateAt = DateTime.Now,
                        BuyerId = buyerId,
                        ProductsJson = "{}"
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
                var productsDict = JsonConvert.DeserializeObject<Dictionary<int, int>>(cart.ProductsJson);
                if (productsDict.ContainsKey(productId))
                {
                    productsDict[productId] += quantity;
                }
                else
                {
                    productsDict.Add(productId, quantity);
                }

                cart.ProductsJson = JsonConvert.SerializeObject(productsDict);
                cart.Price += product.Price * quantity;
                cart.UpdateAt = DateTime.Now;

                await _context.SaveChangesAsync();

                // Map to CartDTO
                var cartDto = _mapper.Map<CartDTO>(cart);

                // Optionally, populate the ProductDTO objects in the cart DTO

                foreach (var kvp in productsDict)
                {
                    var productDto = await _context.Products
                        .Where(p => p.Id == kvp.Key)
                        .ProjectTo<ProductDTO>(_mapper.ConfigurationProvider)
                        .FirstOrDefaultAsync();

                    if (productDto != null)
                    {
                        cartDto.Products.Add(kvp.Value, productDto);
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
                var productsDict = JsonConvert.DeserializeObject<Dictionary<int, int>>(cart.ProductsJson);
                if (!productsDict.ContainsKey(productId))
                {
                    throw new Exception($"Product with ID {productId} not found in the cart");
                }

                // Remove the product from the cart or decrease its quantity
                if (productsDict[productId] > quantity)
                {
                    productsDict[productId] -= quantity;
                }
                else
                {
                    productsDict.Remove(productId);
                }

                cart.ProductsJson = JsonConvert.SerializeObject(productsDict);
                cart.Price -= product.Price * quantity;
                cart.UpdateAt = DateTime.Now;

                await _context.SaveChangesAsync();

                // Map to CartDTO
                var cartDto = _mapper.Map<CartDTO>(cart);

                // Optionally, populate the ProductDTOobjects in the cart DTO
                foreach (var kvp in productsDict)
                {
                    var productDto = await _context.Products
                        .Where(p => p.Id == kvp.Key)
                        .ProjectTo<ProductDTO>(_mapper.ConfigurationProvider)
                        .FirstOrDefaultAsync();

                    if (productDto != null)
                    {
                        cartDto.Products.Add(kvp.Value, productDto);
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