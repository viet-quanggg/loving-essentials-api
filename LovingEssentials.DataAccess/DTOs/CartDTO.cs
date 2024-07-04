using LovingEssentials.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LovingEssentials.DataAccess.DTOs
{
    public class CartDTO
    {
        public int Id { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public int BuyerId { get; set; }
        public int ProductId { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public List<CartItemDTO> Products { get; set; } = new List<CartItemDTO>();
    }
    public class CartItemDTO
    {
        public int Quantity { get; set; }
        public ProductDTO Product { get; set; }
    }
}
