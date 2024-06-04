using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class UserRegister
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Password { get; set; }
        public string Address { get; set; }

        public string City { get; set; }

        public string Country { get; set; }
        public string PhoneNum { get; set; }

        public string Email { get; set; }

        public byte[] Image { get; set; }

        public Guid UserId { get; set; }
        public UserRegister() { }   

        
    }
}
