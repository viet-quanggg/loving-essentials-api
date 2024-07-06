using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LovingEssentials.DataAccess.DTOs.Admin
{
    public class StoreDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public TimeSpan OpenHours { get; set; }
        public TimeSpan CloseHours { get; set; }
    }
}
