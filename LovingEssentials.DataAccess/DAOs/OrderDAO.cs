using AutoMapper;
using LovingEssentials.BusinessObject;
using LovingEssentials.DataAccess.DTOs;
using LovingEssentials.DataAccess.DTOs.Shipper;
using Microsoft.EntityFrameworkCore;
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
                var orders = await _context.Orders.ToListAsync();
                var result = _mapper.Map<List<OrderDTO>>(orders);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<List<OrderDTO>> GetOrdersByUserId(int userId)
        {
            try
            {
                var orders = await _context.Orders.Where(o => o.BuyerId == userId).ToListAsync();
                var result = _mapper.Map<List<OrderDTO>>(orders);

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
    }
}
