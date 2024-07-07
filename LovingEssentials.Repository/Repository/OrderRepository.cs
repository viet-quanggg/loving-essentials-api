using Azure.Core;
using LovingEssentials.BusinessObject;
using LovingEssentials.DataAccess.DAOs;
using LovingEssentials.DataAccess.DTOs;
using LovingEssentials.DataAccess.DTOs.Shipper;
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

        public async Task<List<OrderDetailDTO>> GetOrderDetailsById(int orderid)
        {
            return await _orderDAO.GetOrderDetailsById(orderid);
        }
        public async Task<List<OrderResponse>> GetOrdersByShipperId(int shipperId, OrderStatus? status = null, string buyerName = null, string productName = null)
        {
            return await _orderDAO.GetOrdersByShipperId(shipperId, status, buyerName);
        }

        public async Task<bool> UpdateOrderStatusToProcessing(int orderId)
        {
            return await _orderDAO.UpdateOrderStatusToProcessing(orderId);
        }


        public async Task<bool> UpdateOrderStatusByShipper(UpdateStatusRequest request)
        {
            return await _orderDAO.UpdateOrderStatusByShipper(request);
        }
        public async Task<bool> AddOrderByCartId(int cartId, int addressId, int method, int payment)
        {
            return await _orderDAO.AddOrderByCartId(cartId, addressId, method, payment);
        }
    }
}
