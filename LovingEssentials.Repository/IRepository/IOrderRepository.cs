using LovingEssentials.BusinessObject;
using LovingEssentials.DataAccess.DTOs;
using LovingEssentials.DataAccess.DTOs.Shipper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LovingEssentials.Repository.IRepository
{
    public interface IOrderRepository
    {
        Task<List<OrderDTO>> GetOrders();
        Task<List<OrderDTO>> GetOrdersByUserId(int userId);

        Task<List<OrderDetailDTO>> GetOrderDetailsById(int orderid);

        Task<List<OrderResponse>> GetOrdersByShipperId(int shipperId, OrderStatus? status = null, string buyerName = null, string productName = null);
        Task<bool> UpdateOrderStatusToProcessing(int orderId);
        Task<bool> UpdateOrderStatusByShipper(UpdateStatusRequest request);
        Task<bool> AddOrderByCartId(int cartId, int addressId);

    }
}
