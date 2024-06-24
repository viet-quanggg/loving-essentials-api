using AutoMapper;
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
    }
}
