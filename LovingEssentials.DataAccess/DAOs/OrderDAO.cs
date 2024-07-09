using AutoMapper;
using AutoMapper.QueryableExtensions;
using LovingEssentials.BusinessObject;
using LovingEssentials.DataAccess.DTOs;
using LovingEssentials.DataAccess.DTOs.Shipper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LovingEssentials.DataAccess.DAOs
{
    public class OrderDAO
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public OrderDAO(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<OrderDTO>> GetOrders()
        {
            try
            {
                var orders = await _context.Orders.Include(o => o.OrderDetails).ToListAsync();
                var result = _mapper.Map<List<OrderDTO>>(orders);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<List<OrderDTO>> GetOrdersByUserId(int userid)
        {
            try
            {
                var orders = await _context.Orders.Where(o => o.BuyerId == userid)
                    .Include(o => o.Shippers)
                    .Include(o => o.OrderDetails)
                    .ToListAsync();
                var result = _mapper.Map<List<OrderDTO>>(orders);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<OrderDetailDTO>> GetOrderDetailsById(int orderid)
        {
            try
            {
                var orders = await _context.OrderDetails.Where(o => o.OrderId == orderid)
                    .Include(od => od.Products)
                    .ToListAsync();
                var result = _mapper.Map<List<OrderDetailDTO>>(orders);
              return result;
            }
            catch (Exception ex)
            {
              throw new Exception(ex.Message);
            }
        }
        public async Task<List<OrderResponse>> GetOrdersByShipperId(int shipperId, OrderStatus? status = null, string buyerName = null, string productName = null)
        {
            try
            {
                var query = _context.Orders
                    .Include(o => o.Buyers)
                    .Include(o => o.OrderDetails)
                        .ThenInclude(od => od.Products)
                    .Include(o => o.Address)
                    .AsQueryable();

                query = query.Where(o => o.ShipperId == shipperId);

                if (status.HasValue)
                {
                    query = query.Where(o => o.Status == status.Value);
                }

                if (!string.IsNullOrWhiteSpace(buyerName))
                {
                    query = query.Where(o => o.Buyers.Name.Contains(buyerName) || o.OrderDetails.Any(od => od.Products.Name.Contains(buyerName)));
                }

                if (!string.IsNullOrWhiteSpace(productName))
                {
                    query = query.Where(o => o.OrderDetails.Any(od => od.Products.Name.Contains(productName)));
                }

                var orders = await query.ToListAsync();
                var result = _mapper.Map<List<OrderResponse>>(orders);

                return result;
            }
            catch (Exception ex)
            {
                 throw new Exception($"Error retrieving data from the database: {ex.Message}");
            }
        }

        public async Task<bool> UpdateOrderStatusToProcessing(int orderId)
        {
            try
            {
                var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == orderId);
                if (order == null)
                {
                    return false;
                }
                order.Status = OrderStatus.Processing;
                _context.Orders.Update(order);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating order status to Processing: {ex.Message}");
            }
        }

        public async Task<bool> UpdateOrderStatusByShipper(UpdateStatusRequest request)
        {
            try
            {
                var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == request.orderId && o.ShipperId == request.shipperId);
                if (order == null)
                {
                    return false;
                }
                order.Status = request.newStatus;
                _context.Orders.Update(order);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating order status: {ex.Message}");
            }
        }
        public async Task<bool> AddOrderByCartId(int cartId, int addressId, int method, int payment)
        {
            try
            {
                var cart = await _context.Carts.FirstOrDefaultAsync(o => o.Id == cartId);
                var paymentMethod = new Payment();
                var deliveryMethod = new DeliveryMethod();
                if (method == 1)
                {
                    paymentMethod = Payment.BankTransfer;
                } else if (method == 0)
                {
                    paymentMethod = Payment.Cash;
                } else
                {
                    return false;
                }
                if (payment == 1)
                {
                    deliveryMethod = DeliveryMethod.Delivery;
                } else if (payment == 0)
                {
                    deliveryMethod = DeliveryMethod.TakeAtStore;
                } else
                {
                    return false;
                }
                if (cart == null)
                {
                    return false;
                }

                var order = new Order()
                {
                    Created = DateTime.Now,
                    Updated = DateTime.Now,
                    TotalPrice = cart.Price,
                    BuyerId = cart.BuyerId,
                    ShipperId = null,
                    AddressId = addressId,
                    Status = OrderStatus.Pending,
                    Payment = paymentMethod,
                    DeliveryMethod = deliveryMethod
                };

                await _context.Orders.AddAsync(order);
                await _context.SaveChangesAsync();

                var cartjson = cart.ProductsJson;
                var productsDict = JsonConvert.DeserializeObject<Dictionary<int, int>>(cartjson);

                foreach (var kvp in productsDict)
                {
                    var productDto = await _context.Products
                        .Where(p => p.Id == kvp.Key)
                        .ProjectTo<ProductDTO>(_mapper.ConfigurationProvider)
                        .FirstOrDefaultAsync();

                    var orderdetail = new OrderDetail()
                    {
                        OrderId = order.Id,
                        CreateAt = DateTime.Now,
                        UpdateAt = DateTime.Now,
                        Price = productDto.Price,
                        ProductId = productDto.Id,
                        Quantity = kvp.Value
                    };

                    await _context.OrderDetails.AddAsync(orderdetail);
                    await _context.SaveChangesAsync();
                }

                _context.Carts.Remove(cart);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating order status: {ex.Message}");
            }
        }
    }
}
