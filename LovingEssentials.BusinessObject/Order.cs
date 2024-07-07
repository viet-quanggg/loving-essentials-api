using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LovingEssentials.BusinessObject
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public double TotalPrice { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public Payment Payment { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }

        public int BuyerId { get; set; }
        public User Buyers { get; set; }

        public int? ShipperId { get; set; }
        public User Shippers { get; set; }

        public int AddressId { get; set; }
        public Address Address { get; set; }

        
        
        
        [JsonIgnore]
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

    public enum DeliveryMethod
    {
        TakeAtStore,
        Delivery
    }
    public enum Payment
    {
        Cash,
        BankTransfer
    }
}
