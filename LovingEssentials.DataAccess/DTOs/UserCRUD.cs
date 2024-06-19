using LovingEssentials.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LovingEssentials.DataAccess.DTOs
{
    /*public enum Role
    {
        [EnumMember(Value = "Client")]
        Client = 1,
        [EnumMember(Value = "Shipper")]
        Shipper = 2,
        [EnumMember(Value = "Admin")]
        Admin = 3
    }*/
    public class UserCRUD
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public Role Role {  get; set; }
        public byte Status { get; set; }
    }
}
