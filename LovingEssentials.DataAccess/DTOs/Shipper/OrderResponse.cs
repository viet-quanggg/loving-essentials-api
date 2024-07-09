using LovingEssentials.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LovingEssentials.DataAccess.DTOs.Shipper
{
    public class OrderResponse
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public double TotalPrice { get; set; }
        public string Address { get; set; }
        public int BuyerId { get; set; }
        public int ShipperId { get; set; }
        public OrderStatus Status { get; set; }
        public UserProfileDTO Buyers { get; set; }
        public ICollection<OrderDetailResponse> OrderDetails { get; set; }
    }
}
