using LovingEssentials.BusinessObject;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LovingEssentials.DataAccess.DTOs.Shipper
{
    public class UpdateStatusRequest
    {
        [Required]
        public int orderId { get; set; }

        [Required]
        public int shipperId { get; set; }

        [Required]
        public OrderStatus newStatus { get; set; }
    }
}
