using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LovingEssentials.BusinessObject
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public double TotalPrice { get; set; }
        public int BuyerId { get; set; }
        public int ShipperId { get; set; }
        public OrderStatus Status { get; set; }
        public User Buyers { get; set; }
        public User Shippers { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }

    public enum OrderStatus
    {
        Pending = 1,
        Processing = 2,
        Shipped = 3,
        Delivered = 4,
        Cancelled = 5
    }
}
