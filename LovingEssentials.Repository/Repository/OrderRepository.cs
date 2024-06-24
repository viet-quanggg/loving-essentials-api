using LovingEssentials.BusinessObject;
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
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderDAO _orderDAO;

        public OrderRepository(OrderDAO orderDAO)
        {
            _orderDAO = orderDAO;
        }
        public async Task<List<OrderDTO>> GetOrders()
        {
            return await _orderDAO.GetOrders();
        }
        public async Task<List<OrderDTO>> GetOrdersByUserId(int userId)
        {
            return await _orderDAO.GetOrdersByUserId(userId);
        }
    }
}
