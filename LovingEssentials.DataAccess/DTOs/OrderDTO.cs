using LovingEssentials.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LovingEssentials.DataAccess.DTOs
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public double TotalPrice { get; set; }
        public int BuyerId { get; set; }
        public string ShipperName { get; set; }
    }
}
