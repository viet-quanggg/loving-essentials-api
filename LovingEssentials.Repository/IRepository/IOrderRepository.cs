using LovingEssentials.DataAccess.DTOs;
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
    }
}
