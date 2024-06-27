using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LovingEssentials.DataAccess.DTOs.Admin
{
    public class EditProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string ImageURL { get; set; }
        public int Quantity { get; set; }
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
        public byte Status { get; set; }
    }
}
