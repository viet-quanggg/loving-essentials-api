using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LovingEssentials.DataAccess.DTOs
{
    public class AuthResponse
    {
        public bool Success { get; set; }
        public List<string> Errors { get; set; }
        public string Token { get; set; }

        public AuthResponse()
        {
            Errors = new List<string>();
        }
    }
}
