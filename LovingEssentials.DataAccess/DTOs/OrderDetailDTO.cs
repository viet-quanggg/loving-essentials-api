using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LovingEssentials.DataAccess.DTOs
{
    public class OrderDetailDTO
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string Url { get; set; }
        public string ProductName {  get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
    }
}
